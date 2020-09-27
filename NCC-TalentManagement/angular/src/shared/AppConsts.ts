export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish
    static googleClientAppId : string ;//clientId
    static valueLanguage : string ;

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'NCCTalentManagement'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };
}
