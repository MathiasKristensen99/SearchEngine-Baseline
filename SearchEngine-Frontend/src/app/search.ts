import {Document} from "./document";

export interface Search {
  elapsedMilliseconds: number;
  ignoredTerms: string[];
  documents: Document[];
  hostName: string;
}
