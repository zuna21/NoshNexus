export interface IChartCard {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  ButtonIcon: string;
}

export interface IWeekDayOrder {
  name: string;
  value: number;
}

export interface ITopTenMenuItem {
  name: string;
  value: number;
}

export interface IWeekOrderByHour {
  name: string;
  series: {
    name: string;
    value: number;
  }[];
}
