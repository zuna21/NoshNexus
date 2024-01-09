import { IChartCard } from "../_interfaces/IChart";

export const ALL_CHARTS: IChartCard[] = [
    {
        id: 1,
        title: "Narudzbe po danu",
        description: "Pogledajte narudzbe u vasem lokalu po danu, od ponedeljka do nedelje",
        ButtonIcon: 'bar_chart_4_bars',
        imageUrl: 'http://localhost:5000/images/charts/column-chart.jpg'
    },
    {
        id: 2,
        title: "Najprodavanije",
        description: "Pogledajte vase najprodavanije narudzbe",
        ButtonIcon: 'pie_chart',
        imageUrl: 'http://localhost:5000/images/charts/pie-chart.jpg'
    }
]