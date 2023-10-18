import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IMessagesForMenu } from '../_interfaces/IMessage';

const BASE_URL: string = `${environment.apiUrl}/message`;

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(private http: HttpClient) {}

  getOwnerMessagesForMenu(): Observable<IMessagesForMenu> {
    return this.http.get<IMessagesForMenu>(
      `${BASE_URL}/get-owner-messages-for-menu`
    );
  }
}
