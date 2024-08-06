import {AutentifikacijaHelper} from "./_helpers/autentifikacija-helper";
import {AutentifikacijaToken} from "./_helpers/login-informacije";

export class MojConfig{
  static adresa_servera = "https://localhost:7058";

  static http_opcije = function () {
    // @ts-ignore
    let autentifikacijaToken:AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if(autentifikacijaToken!=null)
      mojtoken=autentifikacijaToken.vrijednost;
    return{
      headers: {
        'autentifikacija-token': mojtoken,
      }
    };
  }
}
