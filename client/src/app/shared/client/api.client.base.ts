import { Response, RequestOptionsArgs } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/empty';

import { Router } from '@angular/router';

export class BaseClient {

  protected transformOptions(options: RequestOptionsArgs): Promise<RequestOptionsArgs> {

    var token = localStorage.getItem('token');
    if(token != null){
      options.headers.append('Authorization', 'Bearer ' + token);
    }
    return Promise.resolve(options);
  }



  protected transformResult(url: string, response: Response, processor: (response: Response) => any): Observable<any> {

    if (response.status === 401) {
      console.warn("The user is not authenticated");
    }

    return processor(response);
  }
}

