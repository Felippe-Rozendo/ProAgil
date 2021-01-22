import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  Url= environment.baseUrl
  eventos: any = [];
  imgAltura = 50;
  imgMargem = 2;
  mostrarImgem = false;
  
  eventosFiltrados: any = [];
  _filtro : string;
  
  get filtro(): string {
    return this._filtro;
  }  
  set filtro(value: string){
    this._filtro = value;
    this.eventosFiltrados = this.filtro ? this.filtrarEventos(this.filtro) : this.eventos;
  }
  
  filtrarEventos(filtrarPor : string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  } 

  constructor( private http: HttpClient ) { }

  ngOnInit() {
    this.getEventos()
  }

  showImagem(){this.mostrarImgem = !this.mostrarImgem;
  }

  getEventos() {
    this.http.get(this.Url + '/api/Evento').subscribe(
      Response => { 
      this.eventos = Response;
    },error => {
      console.error(error);
    }
    );
  }

}
