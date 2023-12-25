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
  private chatId = new BehaviorSubject<number | null>(null);
  $chatId = this.chatId.asObservable();

  constructor(private http: HttpClient) {}

  setChatId(chatId: number | null) {
    this.chatId.next(chatId);
  }

  getChatsForMenu(): Observable<IChatMenu> {
    return this.http.get<IChatMenu>(`http://localhost:5000/api/chats/get-chats-for-menu`);
  }

  getChats(sqName: string = ""): Observable<IChatPreview[]> {
    return this.http.get<IChatPreview[]>(`http://localhost:5000/api/chats/get-chats`);
  }

  createMessage(chatId: number, message: number): Observable<IMessage> {
    return this.http.post<IMessage>(`http://localhost:5000/api/chats/create-message/${chatId}`, message);
  }

  getChat(chatId: number): Observable<IChat> {
    return this.http.get<IChat>(`http://localhost:5000/api/chats/get-chat/${chatId}`);
  }

  markAllAsRead(): Observable<boolean> {
    return this.http.get<boolean>(`http://localhost:5000/api/chats/mark-all-as-read`);
  }


  getUsersForChatParticipants(
    sq: string
  ): Observable<IChatParticipant[]> {
    return this.http.get<IChatParticipant[]>(
      `http://localhost:5000/api/chats/get-users-for-chat-participants?sq=${sq}`
    );
  }

  createChat(chat: ICreateChat): Observable<IChat> {
    return this.http.post<IChat>(`http://localhost:5000/api/chats/create-chat`, chat);
  }

  updateChat(chatId: number, chat: ICreateChat): Observable<IChat> {
    return this.http.put<IChat>(`http://localhost:5000/api/chats/update/${chatId}`, chat);
  }

  removeParticipant(chatId: number, participantId: number) : Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/chats/remove-participant/${chatId}/${participantId}`);
  }

  deleteChat(chatId: number): Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/chats/delete-chat/${chatId}`);
  }
}
