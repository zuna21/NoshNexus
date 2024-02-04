import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  IChat,
  IChatMenu,
  IChatParticipant,
  IChatPreview,
  ICreateChat,
  IMessage,
} from '../_interfaces/IChat';
import { environment } from 'src/environments/environment';

const BASE_URL: string = `${environment.apiUrl}/user/chats`;

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
    return this.http.get<IChatMenu>(`${BASE_URL}/get-chats-for-menu`);
  }

  getChats(sqName: string = ""): Observable<IChatPreview[]> {
    return this.http.get<IChatPreview[]>(`${BASE_URL}/get-chats`);
  }

  createMessage(chatId: number, message: number): Observable<IMessage> {
    return this.http.post<IMessage>(`${BASE_URL}/create-message/${chatId}`, message);
  }

  getChat(chatId: number): Observable<IChat> {
    return this.http.get<IChat>(`${BASE_URL}/get-chat/${chatId}`);
  }

  markAllAsRead(): Observable<boolean> {
    return this.http.get<boolean>(`${BASE_URL}/mark-all-as-read`);
  }


  getUsersForChatParticipants(
    sq: string
  ): Observable<IChatParticipant[]> {
    return this.http.get<IChatParticipant[]>(
      `${BASE_URL}/get-users-for-chat-participants?sq=${sq}`
    );
  }

  createChat(chat: ICreateChat): Observable<IChat> {
    return this.http.post<IChat>(`${BASE_URL}/create-chat`, chat);
  }

  updateChat(chatId: number, chat: ICreateChat): Observable<IChat> {
    return this.http.put<IChat>(`${BASE_URL}/update/${chatId}`, chat);
  }

  removeParticipant(chatId: number, participantId: number) : Observable<number> {
    return this.http.delete<number>(`${BASE_URL}/remove-participant/${chatId}/${participantId}`);
  }

  deleteChat(chatId: number): Observable<number> {
    return this.http.delete<number>(`${BASE_URL}/delete-chat/${chatId}`);
  }
}
