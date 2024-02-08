import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ITable } from 'src/app/_interfaces/ITable';
import { QRCodeModule } from 'angularx-qrcode';

@Component({
  selector: 'app-table-qr-code',
  standalone: true,
  imports: [
    CommonModule,
    QRCodeModule
  ],
  templateUrl: './table-qr-code.component.html',
  styleUrls: ['./table-qr-code.component.css']
})
export class TableQrCodeComponent {
  @Input('table') table?: ITable = {
    id: 21,
    name: "prvi sto"
  }
}
