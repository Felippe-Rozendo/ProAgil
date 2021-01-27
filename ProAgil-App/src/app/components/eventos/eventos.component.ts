import { Component, OnInit } from '@angular/core';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
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
    private modalService: BsModalService
  ) {
    this.localeService.use('pt-br');
  }

  eventos: Evento[];
  evento: Evento;

  imgAltura = 50;
  imgMargem = 2;
  mostrarImagem = false;

  modalRef: BsModalRef;

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
          },
          (error) => { console.error(error) }
        );
      } else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
        this.eService.updateEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          },
          (error) => { console.error(error) }
        );
      }
    }
  }

  bodyDeletarEvento = '';
  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  confirmeDelete(template: any) {
    this.eService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
      }, error => {
        console.log(error);
      }
    );
  }

}
