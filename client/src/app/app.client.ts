import { Injectable } from '@angular/core';

import { Event } from './models/event.model'

@Injectable()
export class Client {
    
    getAnnouncements(): string[]{
        let tempAnnouncements: string[] = [
            "this is the first announcement",
            "this is the second announcement",
            "this is the third announcement that is much longer than the others, mainly to test line wrap"
        ];
        return tempAnnouncements;
    }

    getEvents(): Event[]{
        let tempEvents: Event[] = [
            {
                title: "Event1",
                description: "Description1",
                date: new Date("January 25, 2018"),
            },
            {
                title: "Event2",
                description: "Description2",
                date: new Date("January 26, 2018"),
            },
            {
                title: "Event3",
                description: "Description3",
                date: new Date("January 27, 2018"),
            }
        ];
        return tempEvents;
    }
}