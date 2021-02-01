import { Component, OnInit, ViewChild } from '@angular/core';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {

  ngOnInit() {
    this.getEventos();
    this.validacao();
  }

  constructor(
    private eService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  title = 'Eventos';
  eventos: Evento[];
  evento: Evento;
  dataEvento: string;;

  imgAltura = 50;
  imgMargem = 2;
  mostrarImagem = false;

  deleteRef: BsModalRef;

  registerForm: FormGroup;

  eventosFiltrados: Evento[];
  _filtro: string;

  get filtro(): string {
    return this._filtro;
  }
  set filtro(value: string) {
    this._filtro = value;
    this.eventosFiltrados = this.filtro ? this.filtrarEventos(this.filtro) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  showImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.eService.getEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  validacao() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imageURL: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    })
  }

  modo: string;
  eventoEdit(evento: Evento, template: any) {
    this.modo = 'put'
    this.openModal(template)
    this.evento = Object.assign({}, evento);
    this.registerForm.patchValue(evento)
  }
  novoEvento(template: any) {
    this.modo = 'post'
    this.openModal(template);
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modo === 'post') {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eService.postEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Evento criado com sucesso!');
          },
          (erro: any) => {
            console.error(erro);
          }
        );
      } else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
        this.eService.updateEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com sucesso!');
          },
          (erro: any) => {
            console.error(erro);
          }
        );
      }
    }
  }

  @ViewChild('confirm') confirm;
  eventoSelecionado;

  excluirEvento(evento) {
    this.eventoSelecionado = evento;
    this.deleteRef = this.modalService.show(this.confirm, { class: 'modal-sm' });
  }

  confirmDelete() {
    this.eService.deleteEvento(this.eventoSelecionado.id).subscribe(
      () => {
        console.error();
      },
      () => {
        this.getEventos();
        this.deleteRef.hide();
        this.toastr.success('Excluido com sucesso!');
      }
    );
  }

  decline() {
    this.deleteRef.hide();
  }




}
