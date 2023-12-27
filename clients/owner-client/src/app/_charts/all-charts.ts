import { IChartCard } from '../_interfaces/IChart';

export const ALL_CHARTS: IChartCard[] = [
  {
    id: 1,
    title: 'Weekly Restaurant Order Analysis',
    description:
      'This comprehensive chart provides a detailed analysis of daily restaurant orders throughout the week, offering valuable insights into customer behavior and order patterns. The horizontal axis represents each day of the week, starting from Monday and concluding with Sunday, while the vertical axis quantifies the number of orders received by the restaurant.',
    imageUrl: 'http://localhost:5000/images/charts/week-day-orders.png',
    ButtonIcon: 'equalizer',
  },
  {
    id: 2,
    title: "Top 10 Menu Items - Order Distribution Pie Chart",
    description: "Dive into the culinary preferences of our patrons with this visually engaging pie chart showcasing the top 10 most ordered menu items at our restaurant. Each segment represents a distinct menu item, and the size of each slice corresponds to the frequency of orders for that particular dish.",
    imageUrl: 'http://localhost:5000/images/charts/top-ten-menu-items.png',
    ButtonIcon: 'pie_chart'
  }
];
