export interface Review{
    Id: string;
    FK_AlbumId: string;
    FK_UserId: string;
    UserName?: string;
    Rating: number;
    Comment: string;
}

