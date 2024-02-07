import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';

@Component({
  selector: 'app-tables-qr-code',
  standalone: true,
  imports: [
    CommonModule,
    QRCodeModule
  ],
  templateUrl: './tables-qr-code.component.html',
  styleUrls: ['./tables-qr-code.component.css']
})
export class TablesQrCodeComponent {
}
