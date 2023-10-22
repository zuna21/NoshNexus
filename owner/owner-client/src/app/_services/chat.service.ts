import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IChat, IChatParticipant, IChatPreview } from '../_interfaces/IChat';
import { IChatMenu } from '../_interfaces/IMessage';

const BASE_URL: string = `${environment.apiUrl}/chat`;

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private chatId = new BehaviorSubject<string | null>(null);
  $chatId = this.chatId.asObservable();

  constructor(private http: HttpClient) { }

  setChatId(chatId: string | null) {
    this.chatId.next(chatId);
  }

  getOwnerChatForMenu(): Observable<IChatMenu> {
    return this.http.get<IChatMenu>(`${BASE_URL}/get-owner-chat-for-menu`);
  }


  // odavde je sa novim interfaceom
  getOwnerChats(): Observable<IChatPreview[]> {
    return this.http.get<IChatPreview[]>(`${BASE_URL}/get-owner-chats`);
  }

  getOwnerChat(chatId: string): Observable<IChat> {
    return this.http.get<IChat>(`${BASE_URL}/get-owner-chat/${chatId}`);
  }

}
