export interface IImageCard {
    id: number | string;
    url: string;
    size: number;
}

export interface IChangeProfileImage {
    newProfileImage: IImageCard,
    oldProfileImage: IImageCard
}