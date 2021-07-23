import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Address } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPagableService } from '@core/ipagable-service';
import { EntityPage } from '@core/entity-page';

@Injectable({
  providedIn: 'root'
})
export class AddressService implements IPagableService<Address> {

  uniqueIdentifierName: string = "addressId";

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  getPage(options: { pageIndex: number; pageSize: number; }): Observable<EntityPage<Address>> {
    return this._client.get<EntityPage<Address>>(`${this._baseUrl}api/address/page/${options.pageSize}/${options.pageIndex}`)
  }

  public get(): Observable<Address[]> {
    return this._client.get<{ addresses: Address[] }>(`${this._baseUrl}api/address`)
      .pipe(
        map(x => x.addresses)
      );
  }

  public getById(options: { addressId: string }): Observable<Address> {
    return this._client.get<{ address: Address }>(`${this._baseUrl}api/address/${options.addressId}`)
      .pipe(
        map(x => x.address)
      );
  }

  public remove(options: { address: Address }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/address/${options.address.addressId}`);
  }

  public create(options: { address: Address }): Observable<{ address: Address }> {
    return this._client.post<{ address: Address }>(`${this._baseUrl}api/address`, { address: options.address });
  }
  
  public update(options: { address: Address }): Observable<{ address: Address }> {
    return this._client.put<{ address: Address }>(`${this._baseUrl}api/address`, { address: options.address });
  }
}
