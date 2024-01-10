import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { IVerticalBarChart } from 'src/app/_interfaces/IChart';

@Component({
  selector: 'app-vertical-bar-chart',
  standalone: true,
  imports: [
    CommonModule,
    NgxChartsModule
  ],
  templateUrl: './vertical-bar-chart.component.html',
  styleUrls: ['./vertical-bar-chart.component.css']
})
export class VerticalBarChartComponent {
  @Input('xLabel') xLabel: string = 'Dani u Sedmici'; 
  @Input('yLabel') yLabel: string = 'Broj Narudzbi';
  @Input('chartData') chartData: IVerticalBarChart[] = [
    {
      "name": "Ponedeljak",
      "value": 322
    },
    {
      "name": "Utorak",
      "value": 131
    },
    {
      "name": "Srijeda",
      "value": 232
    },
    {
      "name": "Cetvrtak",
      "value": 234
    },
    {
      "name": "Petak",
      "value": 544
    },
    {
      "name": "Subota",
      "value": 343
    },
    {
      "name": "Nedelja",
      "value": 553
    }
  ];


  // view: [number, number] = [ 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = this.xLabel;
  showYAxisLabel = true;
  yAxisLabel = this.yLabel;

  onSelect(event: Event) {
    console.log(event);
  }

}
