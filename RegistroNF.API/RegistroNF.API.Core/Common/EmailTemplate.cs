namespace RegistroNF.API.Core.Common
{
    public static class EmailTemplate
    {
        public const string EMPRESANOVAPARCIALSUBJECT = "Ação necessária para completar seu cadastro.";
        public const string EMPRESANOVACOMPLETASUBJECT = "Novo cadastro completo.";
        public const string EMPRESAATUALIZADASUBJECT = "Atualização de dados realizada.";
        public const string EMPRESANOTIFICADASUBJECT = "Notificação sobre ausência de dados importantes.";
        public const string EMPRESABLOQUEADASUBJECT = "Bloqueio de empresa até atualização dos dados.";

        public const string BACKBONE =
            @"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset='UTF-8'>
                </head>

                <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial, 
                        sans-serif; font-weight:normal'>

                    <table width='100%' bgcolor='#f4f4f4' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center'>
                                <table width='600' bgcolor='#ffffff' cellpadding='20' cellspacing='0'
                                        style='border-radius:8px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>

                                    {HEADER}

                                    {BODY}

                                    {FOOTER}

                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";


        public const string HEADER = 
            @"<img src=""cid:logo-id""/>";

        public const string EMPRESANOVAPARCIALBODY =
            @"<tr>
                <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                    <div>
                        <p align='center' style='font-size:22px; font-weight:bold'>
                            Olá, {NAME}!
                        </p>
                        <p>
                            Gostaríamos de informar que seu cadastro foi realizado 
                            com êxito e sua empresa de <strong>CNPJ {CNPJ}</strong> 
                            já consta em nosso sistema!
                        </p>
                        <p>
                            Porém, <strong>detectamos a ausência de alguns dados</strong>. 
                            A não atualização dos dados nas próximas utilizações do serviço 
                            poderá acarretar em bloqueio da conta.
                        </p>
                        <p style='font-size:14px;'>
                            A atualização dos dados pode ser realizada pelo mesmo local 
                            de cadastro e emissão de novas notas.
                        </p>
                        <p style='font-size:14px;'>
                            Agradecemos a preferência em utilizar os nossos serviços!
                        </p>
                    </div>
                </td>
            </tr>";

        public const string EMPRESANOVACOMPLETABODY =
            @"<tr>
                <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                    <div>
                        <p align='center' style='font-size:22px; font-weight:bold'>
                            Olá, {NAME}!
                        </p>
                        <p>
						    Gostaríamos de informar que seu cadastro foi realizado
                            com êxito e sua empresa de <strong>CNPJ {CNPJ}</strong> já consta em
                            nosso sistema!
                        </p>                                    
                        <p style='font-weight:bold'>
                            Todas as funcionalidades foram liberadas para utilização.
                        </p>                                    
                        <p style='font-size:14px;'>
                            Agradecemos a preferência em utilizar os nossos serviços!
                        </p>
                    </div>
                </td>
            </tr>";

        public const string EMPRESAATUALIZADABODY =
            @"<tr>
                <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                    <div>
                        <p align='center' style='font-size:22px; font-weight:bold'>
                            Olá, {NAME}!
                        </p>
                        <p>
							Gostaríamos de informar que a atualização dos dados da empresa de
                            <strong>CNPJ {CNPJ}</strong> foi realizada com êxito! 
                        </p>
                        <p>
                            A partir de agora, todas as funcionalidades foram 
                            liberadas e sua empresa encontra-se desbloqueada, 
                            caso tenha sido bloqueada anteriormente.
                        </p>
                        <p style='font-size:14px;'>
                            Agradecemos a preferência em utilizar os nossos serviços!
                        </p>
                    </div>
                </td>
            </tr>";

        public const string EMPRESANOTIFICADABODY =
            @"<tr>
                <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                    <div>
                        <p align='center' style='font-size:22px; font-weight:bold'>
                            Olá, {NAME}!
                        </p>
                        <p>
							Gostaríamos de informar que a atualização dos dados 
                            da empresa de <strong>CNPJ {CNPJ}</strong> ainda não foi realizada!
                        </p>
                        <p>
                            Informamos também o eventual <strong>bloqueio da empresa</strong> 
                            na próxima utilização do serviço de cálculo de impostos.
                        </p>
                        <p style='font-size:14px;'>
							O bloqueio da empresa impede a utilização de quaisquer
                            serviços ofertados pelo nosso sistema.
                        </p>
                    </div>
                </td>
            </tr>";

        public const string EMPRESABLOQUEADABODY =
            @"<tr>
                <td style='color:#333333; font-size:18px; line-height:1.5; text-align:justify;'>
                    <div>
                        <p align='center' style='font-size:22px; font-weight:bold'>
                            Olá, {NAME}!
                        </p>
                        <p>
							Gostaríamos de informar que sua empresa de <strong>CNPJ {CNPJ}
                            </strong> foi bloqueada pela pendência de dados cadastrais!
                        </p>
                        <p>
                            Com o bloqueio, não é possível utilizar nossos serviços 
                            até atualização dos dados da empresa.
                        </p>
                        <p style='font-size:14px;'>
							A atualização dos dados pode ser realizada pelo mesmo 
                            local de cadastro e emissão de novas notas.
                        </p>
                    </div>
                </td>
            </tr>";

        public const string FOOTER =
            @"<tr bgcolor=""#20293e"">
                <td align='left'>
                    <table cellpadding='0' cellspacing='0'>
                        <tr>
                            <td>
                                <img src=""cid:logomarca-id"" 
                                width='24' height='24' style='display:block;'/>
                            </td>
                            <td style='padding-left:8px; font-size:12px; color:#FFFFFF'>
                                © {YEAR} TaxAPI. All rights reserved.
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr bgcolor=""#20293e"">
                <td align='right' style='font-size:10px; 
                    font-style:italic; padding-top:6px; color:#FFFFFF'>
                    {dd/MM/yyyy} {HH:mm:ss}
                </td>
            </tr>";
    }
}