import { Injectable } from '@angular/core';

import { Event } from './models/event.model'
import { Photo } from './models/photo.model'
import { PhotoGroup } from './models/photo-group.model'

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

    getPhotos(): PhotoGroup[]{
        let photo1 = { id: 1, title: 'Photo1', url: 'assets/photos/2013_group.jpg', sortOrder: 1 };
        let photo2 = { id: 2, title: 'Photo2', url: 'assets/photos/2014_group.jpg', sortOrder: 2 };
        let photo3 = { id: 3, title: 'Photo3', url: 'assets/photos/2015_group.jpg', sortOrder: 3 };
        let photo4 = { id: 4, title: 'Photo4', url: 'assets/photos/2016_group1.jpg', sortOrder: 4 };
        let photo5 = { id: 5, title: 'Photo5', url: 'assets/photos/vertical.png', sortOrder: 5 };
        let photo6 = { id: 6, title: 'Photo6', url: 'assets/photos/2016_group2.jpg', sortOrder: 6 };
        let photo7 = { id: 7, title: 'Photo7', url: 'assets/photos/2016_group3.jpg', sortOrder: 7 };

        let tempEvents: PhotoGroup[] = [
            { groupName : '2013', sortOrder: 4, photos : [ photo1 ] },
            { groupName : '2014', sortOrder: 3, photos : [ photo2 ] },
            { groupName : 'misc', sortOrder: 5, photos : [ photo5 ] },
            { groupName : '2015', sortOrder: 2, photos : [ photo3 ] },
            { groupName : '2016', sortOrder: 1, photos : [ photo4, photo6, photo7 ] },
            
        ];

        tempEvents = tempEvents.sort((x, y) => x.sortOrder - y.sortOrder);
        tempEvents.forEach(x => 
            x.photos = x.photos.sort((x, y) => 
                x.sortOrder - y.sortOrder
            ).reverse());
        
        return tempEvents;
    } 
}