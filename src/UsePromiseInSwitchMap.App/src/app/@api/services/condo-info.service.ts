import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { CondoInfo } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPagableService } from '@core/ipagable-service';
import { EntityPage } from '@core/entity-page';

@Injectable({
  providedIn: 'root'
})
export class CondoInfoService implements IPagableService<CondoInfo> {

  uniqueIdentifierName: string = "condoInfoId";

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  getPage(options: { pageIndex: number; pageSize: number; }): Observable<EntityPage<CondoInfo>> {
    return this._client.get<EntityPage<CondoInfo>>(`${this._baseUrl}api/condoInfo/page/${options.pageSize}/${options.pageIndex}`)
  }

  public get(): Observable<CondoInfo[]> {
    return this._client.get<{ condoInfos: CondoInfo[] }>(`${this._baseUrl}api/condoInfo`)
      .pipe(
        map(x => x.condoInfos)
      );
  }

  public getById(options: { condoInfoId: string }): Observable<CondoInfo> {
    return this._client.get<{ condoInfo: CondoInfo }>(`${this._baseUrl}api/condoInfo/${options.condoInfoId}`)
      .pipe(
        map(x => x.condoInfo)
      );
  }

  public remove(options: { condoInfo: CondoInfo }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/condoInfo/${options.condoInfo.condoInfoId}`);
  }

  public create(options: { condoInfo: CondoInfo }): Observable<{ condoInfo: CondoInfo }> {
    return this._client.post<{ condoInfo: CondoInfo }>(`${this._baseUrl}api/condoInfo`, { condoInfo: options.condoInfo });
  }
  
  public update(options: { condoInfo: CondoInfo }): Observable<{ condoInfo: CondoInfo }> {
    return this._client.put<{ condoInfo: CondoInfo }>(`${this._baseUrl}api/condoInfo`, { condoInfo: options.condoInfo });
  }
}
