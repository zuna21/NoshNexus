import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import {
  IChat,
  IChatMenu,
  IChatParticipant,
  IChatPreview,
  ICreateChat,
  IMessage,
} from '../_interfaces/IChat';

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

  getChatsForMenu(): Observable<IChatMenu> {
    return this.http.get<IChatMenu>(`http://localhost:5000/api/owner/chats/get-chats-for-menu`);
  }

  // odavde je sa novim interfaceom
  getChats(): Observable<IChatPreview[]> {
    return this.http.get<IChatPreview[]>(`http://localhost:5000/api/owner/chats/get-chats`);
  }

  createMessage(chatId: string, message: string): Observable<IMessage> {
    return this.http.post<IMessage>(`http://localhost:5000/api/owner/chats/create-message/${chatId}`, message);
  }

  getChat(chatId: string): Observable<IChat> {
    return this.http.get<IChat>(`http://localhost:5000/api/owner/chats/get-chat/${chatId}`);
  }

  getUsersForChatParticipants(
    searchQuery: string
  ): Observable<IChatParticipant[]> {
    return this.http.get<IChatParticipant[]>(
      `http://localhost:5000/api/owner/chats/get-users-for-chat-participants?sq=${searchQuery}`
    );
  }

  createChat(chat: ICreateChat): Observable<IChat> {
    return this.http.post<IChat>(`http://localhost:5000/api/owner/chats/create-chat`, chat);
  }
}
