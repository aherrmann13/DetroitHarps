import { Observable } from 'rxjs';
import { HttpResponseBase } from '@angular/common/http';

export class BaseClient {

  protected transformOptions(options: HttpResponseBase): Promise<HttpResponseBase> {

    const token = localStorage.getItem('token');
    if (token != null) {
      options.headers.append('Authorization', 'Bearer ' + token);
    }
    return Promise.resolve(options);
  }

  protected transformResult(
    url: string,
    response: HttpResponseBase,
    processor: (response: HttpResponseBase) => any
  ): Observable<any> {
    if (response.status === 401) {
      console.warn('The user is not authenticated');
    }

    return processor(response);
  }
}

