import { Observable } from 'rxjs';
import { HttpHeaders, HttpRequest, HttpResponseBase } from '@angular/common/http';

export interface NSwagRequest {
  headers: HttpHeaders;
}

export class BaseClient {

  protected transformOptions<T extends NSwagRequest>(request: T): Promise<T> {

    const token = localStorage.getItem('token');
    if (token != null) {
      const headers: HttpHeaders = request.headers.append('Authorization', 'Bearer ' + token);
      return Promise.resolve({...request, headers: headers });
      
    } else {
      return Promise.resolve(request);
    }
    
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

