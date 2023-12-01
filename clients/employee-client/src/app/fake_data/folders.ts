import { IFolder, IFolderWindowFolder } from "../_interfaces/IFolder";

export const ROOT_FOLDERS_DATABASE: IFolder[] = [
    {
        "id": "1a34b5c6-d789-4e01-a234-567890123456",
        "name": "Folder One",
        "description": "This is the first folder.",
        "belongsToTable": "employee",
        "ownerId": "2b45c6d7-e890-4f12-b345-678901234567",
        "belongsTo": "",
        "createdAt": "2023-10-05T12:34:56Z"
    },
    {
        "id": "2c56d7e8-f901-4g23-c456-789012345678",
        "name": "Folder Two",
        "description": "This is the second folder.",
        "belongsToTable": "employee",
        "ownerId": "2b45c6d7-e890-4f12-b345-678901234567",
        "belongsTo": "",
        "createdAt": "2023-10-05T13:45:00Z"
    },
    {
        "id": "3d67e8f9-g012-4h34-d567-890123456789",
        "name": "Folder Three",
        "description": "This is the third folder.",
        "belongsToTable": "employee",
        "ownerId": "2b45c6d7-e890-4f12-b345-678901234567",
        "belongsTo": "",
        "createdAt": "2023-10-05T14:56:30Z"
    }
];

export const ROOT_FOLDERS: IFolderWindowFolder[] = [
    {
        "id": "1a34b5c6-d789-4e01-a234-567890123456",
        "name": "Folder One",
        "belongsTo": '',
        "description": "This is the first folder.",
        "createdAt": "2023-10-05T12:34:56Z"
    },
    {
        "id": "2c56d7e8-f901-4g23-c456-789012345678",
        "name": "Folder Two",
        "belongsTo": '',
        "description": "This is the second folder.",
        "createdAt": "2023-10-05T13:45:00Z"
    },
    {
        "id": "3d67e8f9-g012-4h34-d567-890123456789",
        "name": "Folder Three",
        "belongsTo": '',
        "description": "This is the third folder.",
        "createdAt": "2023-10-05T14:56:30Z"
    }
];

export const FOLDERS_IN_FOLDER: IFolderWindowFolder[] = [
    {
        "id": "1a34b5c6-d789-4e01-a234-567890123456",
        "name": "In another one",
        "belongsTo": '3d67e8f9-g012-4h34-d567-890123456789',
        "description": "this is some longer description for folder on in another one",
        "createdAt": "2023-10-05T12:34:56Z"
    },
    {
        "id": "2c56d7e8-f901-4g23-c456-789012345678",
        "name": "In another two",
        "belongsTo": '3d67e8f9-g012-4h34-d567-890123456789',
        "description": "This is the second folder.",
        "createdAt": "2023-10-05T13:45:00Z"
    }
];

export const OPENED_FOLDER: IFolderWindowFolder = {
    "id": "1a34b5c6-d789-4e01-a234-567890123456",
    "name": "In another one",
    "belongsTo": '3d67e8f9-g012-4h34-d567-890123456789',
    "description": "this is some longer description for folder on in another one",
    "createdAt": "2023-10-05T12:34:56Z"
}