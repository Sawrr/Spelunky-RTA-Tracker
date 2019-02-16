import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) {}

  public getStatus(roomCode: string) {
    return this.http.get('http://localhost:8080/api/rooms/' + roomCode);
  }
}
