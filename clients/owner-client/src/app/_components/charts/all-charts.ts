import { IChartCard } from "src/app/_interfaces/IChart";

export const ALL_CHARTS : IChartCard[] = [
    {
        id: 1, 
        ButtonIcon: 'chart_bar',
        description: "Pogledajte Vase narudzbe za dane pojedinacno od odabranog raspona datuma",
        title: "Orders By Day",
        imageUrl: 'https://noshnexus.com/images/charts/orders-by-day.png'
    },
    {
        id: 2,
        ButtonIcon: 'pie_chart',
        description: "Pogledajte vasih top 10 proizvoda koji su kupci najvise narucivali",
        imageUrl: 'https://noshnexus.com/images/charts/top-ten-menu-items.png',
        title: "Top Ten Menu Items"
    }
];