import {LoginInformacije} from "./login-informacije";

export class AutentifikacijaHelper {
  static setLoginInfo(x: LoginInformacije | null) :void
  {
    if(x==null)
      x = new LoginInformacije();
    sessionStorage.setItem("autentifikacija-token", JSON.stringify(x));
  }

  static getLoginInfo():LoginInformacije
  {
    // @ts-ignore
    let x:string = sessionStorage.getItem("autentifikacija-token");
    if(x==="")
      return new LoginInformacije();

    try {

      let loginInformacije:LoginInformacije = JSON.parse(x);
      if(loginInformacije==null)
        return new LoginInformacije();
      return loginInformacije;
    }
    catch (e) {
      return new LoginInformacije();
    }
  }
}
