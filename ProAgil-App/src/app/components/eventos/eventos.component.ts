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
  eventos: any;

  constructor( private http: HttpClient ) { }

  ngOnInit() {
    this.getEventos()
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
