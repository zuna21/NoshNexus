export interface IImageCard {
    id: string;
    onClient: boolean;
    url: string;
    size: number;
}

export interface IChangeProfileImage {
    newProfileImage: IImageCard,
    oldProfileImage: IImageCard
}