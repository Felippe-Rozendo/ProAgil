import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root',
})
export class EventoService {
  constructor(private http: HttpClient) {}

  Url = `${environment.baseUrl}/api/evento`;

  getEvento():Observable<Evento[]> {
    return this.http.get<Evento[]>(this.Url);
	}
		
  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.Url}/${id}`);
	}
		
  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.Url}/tema/${tema}`);
	}
  
  postEvento(evento: Evento){
    return this.http.post(this.Url, evento);
  }

  updateEvento(evento: Evento){
    return this.http.put(`${this.Url}/${evento.id}`, evento);
  }
  
  deleteEvento(id: number) {
    return this.http.delete(`${this.Url}/${id}`);
	}


}
