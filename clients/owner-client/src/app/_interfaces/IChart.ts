export interface IChartCard {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  ButtonIcon: string;
}

export interface IPieChart {
  labels: string[];
  data: number[];
}

export interface ILineChart {
  labels: string[];
  data: number[];
}