import { Injectable } from '@angular/core';

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
}