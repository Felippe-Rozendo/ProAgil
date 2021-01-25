import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {
  eventos: Evento[];
  imgAltura = 50;
  imgMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;

  eventosFiltrados: Evento[];
  _filtro: string;

  get filtro(): string {
    return this._filtro;
  }
  set filtro(value: string) {
    this._filtro = value;
    this.eventosFiltrados = this.filtro
      ? this.filtrarEventos(this.filtro)
      : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
    (evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1  
    );
  }

  constructor(private eService: EventoService,
              private modalService: BsModalService
    ) {}

  ngOnInit() {
    this.getEventos();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
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
}