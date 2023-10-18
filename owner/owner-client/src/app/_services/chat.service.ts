import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IChat } from '../_interfaces/IMessage';

const BASE_URL: string = `${environment.apiUrl}/chat`;

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerChat(chatId: string): Observable<IChat> {
    return this.http.get<IChat>(`${BASE_URL}/get-owner-chat/${chatId}`);
  }
}
