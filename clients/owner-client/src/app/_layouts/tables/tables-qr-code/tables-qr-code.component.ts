import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';
import jsPDF from 'jspdf';

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

  async onDownload() {
    // format a4 je 210 x 297 (width x height)
    const table = document.getElementById('qr-wrapper');
    console.log(table);
    if (!table) return;
    const doc = new jsPDF("portrait", "mm", "a4");
    await doc.html(table);
    doc.save("Table");
  }
}
