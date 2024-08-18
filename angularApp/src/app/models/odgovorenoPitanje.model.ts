export interface OdgovorenoPitanje{
  id: string;
  pitanje: string;
  odgovor: string;
  datumKreiranja: Date;
  isExpanded: boolean;
  relativeTime?: string;
}
