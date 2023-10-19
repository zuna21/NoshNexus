import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IChat, IChatMenu } from '../_interfaces/IMessage';

const BASE_URL: string = `${environment.apiUrl}/chat`;

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private chatId = new BehaviorSubject<string | null>(null);
  $chatId = this.chatId.asObservable();

  constructor(private http: HttpClient) {}

  setChatId(chatId: string | null) {
    this.chatId.next(chatId);
  }

  getOwnerChat(chatId: string): Observable<IChat> {
    return this.http.get<IChat>(`${BASE_URL}/get-owner-chat/${chatId}`);
  }

  getOwnerChatForMenu(): Observable<IChatMenu> {
    return this.http.get<IChatMenu>(`${BASE_URL}/get-owner-chat-for-menu`);
  }
}
