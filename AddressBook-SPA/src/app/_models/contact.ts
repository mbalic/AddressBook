import { PhoneNumber } from './phoneNumber';

export interface Contact {
    id: number;
    dateCreated: Date;
    name: string;
    description: string;
    dateOfBirth: Date;
    address: string;
    phoneNumbers: PhoneNumber[];
}
