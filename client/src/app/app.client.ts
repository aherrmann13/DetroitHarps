import { Injectable } from '@angular/core';

import { Event } from './models/event.model'
import { Photo } from './models/photo.model'

@Injectable()
export class Client {
    
    getAnnouncements(): string[]{
        let tempAnnouncements: string[] = [
            'this is the first announcement',
            'this is the second announcement',
            'this is the third announcement that is much longer than the others, mainly to test line wrap'
        ];
        return tempAnnouncements;
    }

    getEvents(): Event[]{
        let tempEvents: Event[] = [
            {
                title: 'Event1',
                description: 'Description1',
                date: new Date('January 25, 2018'),
            },
            {
                title: 'Event2',
                description: 'Description2',
                date: new Date('January 26, 2018'),
            },
            {
                title: 'Event3',
                description: 'Description3',
                date: new Date('January 27, 2018'),
            }
        ];
        return tempEvents;
    }

    getPhotos(): Photo[]{
        let tempEvents: Photo[] = [
            { id: 1, title: 'Photo1', url: 'assets/photos/2013_group.jpg', sortOrder: 1 },
            { id: 2, title: 'Photo2', url: 'assets/photos/2014_group.jpg', sortOrder: 2 },
            { id: 3, title: 'Photo3', url: 'assets/photos/2015_group.jpg', sortOrder: 3 },
            { id: 4, title: 'Photo4', url: 'assets/photos/2016_group1.jpg', sortOrder: 4 },
            { id: 5, title: 'Photo5', url: 'assets/photos/vertical.png', sortOrder: 5 },
            { id: 6, title: 'Photo6', url: 'assets/photos/2016_group2.jpg', sortOrder: 6 },
            { id: 7, title: 'Photo7', url: 'assets/photos/2016_group3.jpg', sortOrder: 7 }
        ];
        return tempEvents;
    } 
}