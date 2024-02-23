import { Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import jsPDF from 'jspdf';
import { PdfService } from './pdf.service';
import { Subscription } from 'rxjs';
import { LoadingService } from 'src/app/_services/loading.service';

@Component({
  selector: 'app-pdf-wrapper',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pdf-wrapper.component.html',
  styleUrls: ['./pdf-wrapper.component.css']
})
export class PdfWrapperComponent implements OnInit, OnDestroy {
  @ViewChild('pdfWrapper') pdfWrapper?: ElementRef;
  @Input("name") name: string = "nosh-nexus";

  downloadSub?: Subscription;

  constructor(
    private pdfService: PdfService,
    private loadingService: LoadingService
  ) {}
  
  ngOnInit(): void {
    this.downloadPdf();
  }

  downloadPdf() {
    this.downloadSub = this.pdfService.download$.subscribe({
      next: download => {
        if (download) this.onDownload();
      }
    });
  }

  async onDownload() {
    if (!this.pdfWrapper) return;
    this.loadingService.busy();
    const content: HTMLElement = this.pdfWrapper.nativeElement;
    const doc = new jsPDF("portrait", "pt", "a4", true);
    await doc.html(content);
    doc.save(this.name);
    this.loadingService.free();
  }

  ngOnDestroy(): void {
    this.downloadSub?.unsubscribe();
  }
}
