export interface IFolder {
    id: string;
    name: string;
    description: string;
    belongsToTable: string;
    ownerId: string;
    belongsTo: string;
    createdAt: string;
}

export interface IFolderWindowFolder {
    id: string;
    belongsTo: string;  // Zbog tipke back
    name: string;
    description: string;
    createdAt: string;
}