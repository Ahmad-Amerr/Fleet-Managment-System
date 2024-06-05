export interface Gvar {
    dicOfDic: { [key: string]: { [key: string]: string } };
    dicOfDT: { [key: string]: any[] }; 
  }
  
  export class Gvar implements Gvar {
    dicOfDic: { [key: string]: { [key: string]: string } } = {};
    dicOfDT: { [key: string]: any[] } = {};
  }
  