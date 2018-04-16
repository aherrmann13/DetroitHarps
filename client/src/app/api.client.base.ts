import { Response, RequestOptionsArgs } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/empty';

export class BaseClient {

  protected transformOptions(options: RequestOptionsArgs): Promise<RequestOptionsArgs> {

    return Promise.resolve(options);
  }



  protected transformResult(url: string, response: Response, processor: (response: Response) => any): Observable<any> {

    if (response) {
    }

    return processor(response);
  }
}

