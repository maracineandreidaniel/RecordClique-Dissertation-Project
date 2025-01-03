export interface Album {
    Id: string;
    Title: string;
    Description: string;
    Cover: string;
    ReleaseDate: Date;
    RecordLabel: string;
    Genres: string[];
    Artists: string[];
    IsFavourite?: boolean;
    IsListening?: boolean;
    IsOnWishlist?: boolean;
}