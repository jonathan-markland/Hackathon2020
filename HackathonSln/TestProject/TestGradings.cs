﻿using System.Collections.Generic;
using System.Linq;
using Xunit;
using MCosmosClassLibrary;
using MCosmosClassLibrary.Models;
using ExampleFilesCollection;

namespace TestProject
{
    public class GradeCalculationScenarios
    {
        [Theory]
        [InlineData(DiscGrade.GradeA, DiscGrade.GradeA, DiscGrade.GradeA)]
        [InlineData(DiscGrade.GradeA, DiscGrade.GradeB, DiscGrade.GradeB)]
        [InlineData(DiscGrade.GradeA, DiscGrade.GradeC, DiscGrade.GradeC)]
        [InlineData(DiscGrade.GradeB, DiscGrade.GradeA, DiscGrade.GradeB)]
        [InlineData(DiscGrade.GradeB, DiscGrade.GradeB, DiscGrade.GradeB)]
        [InlineData(DiscGrade.GradeB, DiscGrade.GradeC, DiscGrade.GradeC)]
        [InlineData(DiscGrade.GradeC, DiscGrade.GradeA, DiscGrade.GradeC)]
        [InlineData(DiscGrade.GradeC, DiscGrade.GradeB, DiscGrade.GradeC)]
        [InlineData(DiscGrade.GradeC, DiscGrade.GradeC, DiscGrade.GradeC)]
        public void Floor(DiscGrade first, DiscGrade second, DiscGrade expected)
        {
            var actual = ToleranceMathematics.Floor(first, second);
            Assert.Equal(expected, actual);
        }



        [Theory]
        [InlineData(0.0     , DiscGrade.GradeA)]
        [InlineData(0.002   , DiscGrade.GradeA)]
        [InlineData(0.002001, DiscGrade.GradeB)]
        [InlineData(0.0025  , DiscGrade.GradeB)]
        [InlineData(0.002501, DiscGrade.GradeC)]
        public void FlatParaGradeFor(double measurement, DiscGrade expected)
        {
            var actual = ToleranceMathematics.FlatParaGradeFor(measurement);
            Assert.Equal(expected, actual);
        }



        [Theory]
        [InlineData(28.020,           DiscGrade.GradeA)]
        [InlineData(28.020 - 0.001,   DiscGrade.GradeA)]
        [InlineData(28.020 + 0.001,   DiscGrade.GradeA)]
        [InlineData(28.020 - 0.00101, DiscGrade.GradeB)]
        [InlineData(28.020 + 0.00101, DiscGrade.GradeB)]
        [InlineData(28.020 - 0.002,   DiscGrade.GradeB)]
        [InlineData(28.020 + 0.002,   DiscGrade.GradeB)]
        [InlineData(28.020 - 0.00201, DiscGrade.GradeC)]
        [InlineData(28.020 + 0.00201, DiscGrade.GradeC)]
        public void DistanceGradeFor(double measurement, DiscGrade expected)
        {
            var actual = ToleranceMathematics.DistanceGradeFor(measurement);
            Assert.Equal(expected, actual);
        }



        [Fact]
        public void StruderFilteredOnOverallGradesAandB()
        {
            // TODO: Prove GradeA -- needs a different dataset because all these are GradeB

            var primaryList = DalesSpreadsheetProvider.GroundAtStruder();

            var filteredList = primaryList.IncludingGradeAandBonly();

            var stringList = filteredList.Select(disc => disc.CSVLineWithOverallGrade()).ToList();

            var expected = new List<string>
            {
                "'008','0.00128','0.00120','0.00119','0.00127','0.00108','0.00151','0.00059','0.00113','28.02057','28.02113','28.02105','28.02199','28.02140','28.02024','28.02007','28.02154','GradeB'",
                "'015','0.00198','0.00084','0.00089','0.00083','0.00168','0.00190','0.00157','0.00223','28.01998','28.02024','28.02095','28.02147','28.01999','28.01907','28.02021','28.02123','GradeB'",
                "'S011','0.00148','0.00171','0.00220','0.00168','0.00089','0.00085','0.00103','0.00097','28.02112','28.02170','28.01980','28.02120','28.02060','28.01980','28.01828','28.01914','GradeB'",
                "'S021','0.00165','0.00150','0.00120','0.00228','0.00138','0.00117','0.00107','0.00152','28.01895','28.01976','28.01883','28.01931','28.02040','28.01936','28.01884','28.02020','GradeB'",
                "'S024','0.00127','0.00160','0.00151','0.00160','0.00109','0.00139','0.00110','0.00139','28.01994','28.02080','28.01934','28.01996','28.02013','28.01937','28.01831','28.01919','GradeB'",
                "'S027','0.00120','0.00153','0.00133','0.00145','0.00122','0.00123','0.00092','0.00163','28.02133','28.02157','28.02107','28.02129','28.02081','28.01985','28.01966','28.02063','GradeB'",
                "'S043','0.00145','0.00182','0.00146','0.00173','0.00180','0.00171','0.00134','0.00184','28.01947','28.01981','28.01989','28.02027','28.01904','28.01849','28.01849','28.01969','GradeB'",
                "'S063','0.00127','0.00082','0.00090','0.00088','0.00146','0.00167','0.00132','0.00202','28.01920','28.01936','28.01998','28.02014','28.01887','28.01801','28.01857','28.01975','GradeB'",
                "'S067','0.00107','0.00107','0.00114','0.00099','0.00104','0.00095','0.00108','0.00145','28.02028','28.02035','28.02052','28.02133','28.01952','28.01858','28.01928','28.02088','GradeB'",
                "'S070','0.00107','0.00100','0.00122','0.00113','0.00057','0.00065','0.00065','0.00112','28.02190','28.01973','28.02075','28.02045','28.02102','28.02108','28.02176','28.02172','GradeB'",
                "'S074','0.00105','0.00099','0.00119','0.00109','0.00063','0.00056','0.00051','0.00069','28.01912','28.01954','28.02002','28.02028','28.01930','28.01860','28.01920','28.02042','GradeB'",
                "'S076','0.00074','0.00092','0.00100','0.00089','0.00062','0.00061','0.00050','0.00090','28.01992','28.01976','28.02042','28.02066','28.01940','28.01824','28.01876','28.02002','GradeB'",
                "'S079','0.00098','0.00085','0.00125','0.00113','0.00087','0.00081','0.00066','0.00078','28.02103','28.02091','28.02137','28.02117','28.01921','28.01819','28.01815','28.01917','GradeB'",
                "'S080','0.00145','0.00096','0.00155','0.00134','0.00050','0.00054','0.00051','0.00091','28.02123','28.02186','28.02137','28.02138','28.02084','28.02046','28.02042','28.02116','GradeB'",
                "'S082','0.00111','0.00132','0.00142','0.00148','0.00070','0.00079','0.00070','0.00113','28.02074','28.02069','28.02103','28.02089','28.01948','28.01830','28.01912','28.02046','GradeB'",
                "'S102','0.00091','0.00105','0.00093','0.00101','0.00061','0.00072','0.00078','0.00127','28.02055','28.02089','28.02132','28.02155','28.01992','28.01932','28.01992','28.02100','GradeB'",
                "'S116','0.00094','0.00129','0.00107','0.00117','0.00062','0.00112','0.00091','0.00126','28.02008','28.02016','28.02054','28.02052','28.01969','28.01837','28.01873','28.02009','GradeB'",
                "'S119','0.00159','0.00200','0.00151','0.00183','0.00124','0.00150','0.00123','0.00139','28.02006','28.02017','28.02099','28.02099','28.01949','28.01865','28.01985','28.02081','GradeB'",
            };

            Assert.Equal(expected.WithDoubleQuotes(), stringList);
        }



        [Fact]
        public void OverallGradesForFullStruderDataset()
        {
            var primaryList = DalesSpreadsheetProvider.GroundAtStruder();

            var stringList = primaryList.Select(disc => disc.CSVLineWithOverallGrade()).ToList();

            var expected = new List<string> 
            {
                "'005D','0.00156','0.00134','0.00144','0.00156','0.00048','0.00061','0.00047','0.00089','28.01701','28.01803','28.01825','28.01747','28.01753','28.01687','28.01732','28.01859','GradeC'",
                "'006','0.00348','0.00136','0.00156','0.00160','0.00069','0.00113','0.00076','0.00122','28.01810','28.01844','28.01864','28.01896','28.01785','28.01735','28.01797','28.01895','GradeC'",
                "'007','0.00141','0.00133','0.00125','0.00127','0.00133','0.00160','0.00101','0.00166','28.02224','28.02276','28.02358','28.02422','28.02322','28.02208','28.02312','28.02452','GradeC'",
                "'008','0.00128','0.00120','0.00119','0.00127','0.00108','0.00151','0.00059','0.00113','28.02057','28.02113','28.02105','28.02199','28.02140','28.02024','28.02007','28.02154','GradeB'",
                "'009','0.00126','0.00134','0.00117','0.00117','0.00090','0.00115','0.00052','0.00144','28.02246','28.02294','28.02304','28.02384','28.02306','28.02232','28.02282','28.02426','GradeC'",
                "'010','0.00178','0.00170','0.00160','0.00152','0.00111','0.00135','0.00072','0.00152','28.02314','28.02316','28.02314','28.02428','28.02364','28.02298','28.02354','28.02450','GradeC'",
                "'011','0.00144','0.00178','0.00152','0.00133','0.00167','0.00208','0.00202','0.00260','28.01215','28.01233','28.01232','28.01316','28.01220','28.01140','28.01201','28.01338','GradeC'",
                "'012','0.00088','0.00105','0.00095','0.00125','0.00128','0.00155','0.00143','0.00215','28.01852','28.01882','28.01824','28.01893','28.01802','28.01740','28.01777','28.01854','GradeC'",
                "'013','0.00104','0.00083','0.00102','0.00104','0.00167','0.00209','0.00156','0.00215','28.02076','28.02135','28.02132','28.02217','28.02137','28.02051','28.02087','28.02217','GradeC'",
                "'014','0.00103','0.00087','0.00086','0.00090','0.00142','0.00172','0.00152','0.00189','28.02230','28.02238','28.02286','28.02332','28.02271','28.02189','28.02170','28.02305','GradeC'",
                "'015','0.00198','0.00084','0.00089','0.00083','0.00168','0.00190','0.00157','0.00223','28.01998','28.02024','28.02095','28.02147','28.01999','28.01907','28.02021','28.02123','GradeB'",
                "'S009','0.00323','0.00291','0.00254','0.00314','0.00208','0.00118','0.00302','0.00142','28.01848','28.02500','28.01686','28.02576','28.02357','28.01704','28.01445','28.01445','GradeC'",
                "'S010','0.00178','0.00188','0.00154','0.00166','0.00096','0.00092','0.00135','0.00107','28.02202','28.02284','28.02012','28.02076','28.02108','28.02027','28.01748','28.01895','GradeC'",
                "'S011','0.00148','0.00171','0.00220','0.00168','0.00089','0.00085','0.00103','0.00097','28.02112','28.02170','28.01980','28.02120','28.02060','28.01980','28.01828','28.01914','GradeB'",
                "'S014','0.00124','0.00121','0.00164','0.00133','0.00178','0.00197','0.00124','0.00166','28.01806','28.01882','28.01552','28.01918','28.01815','28.01747','28.01751','28.01857','GradeC'",
                "'S015','0.00135','0.00161','0.00147','0.00136','0.00152','0.00154','0.00096','0.00187','28.01815','28.01865','28.01815','28.01868','28.01717','28.01677','28.01650','28.01727','GradeC'",
                "'S016','0.00144','0.00143','0.00240','0.00133','0.00116','0.00086','0.00175','0.00161','28.01801','28.01851','28.01743','28.01796','28.01704','28.01642','28.01592','28.01666','GradeC'",
                "'S017','0.00292','0.00144','0.00148','0.00140','0.00140','0.00133','0.00117','0.00150','28.01836','28.01888','28.01789','28.01838','28.01863','28.01809','28.01726','28.01806','GradeC'",
                "'S019','0.00144','0.00159','0.00127','0.00140','0.00098','0.00136','0.00133','0.00154','28.01735','28.01835','28.01641','28.01729','28.01967','28.01913','28.01810','28.01861','GradeC'",
                "'S021','0.00165','0.00150','0.00120','0.00228','0.00138','0.00117','0.00107','0.00152','28.01895','28.01976','28.01883','28.01931','28.02040','28.01936','28.01884','28.02020','GradeB'",
                "'S022','0.00123','0.00143','0.00141','0.00153','0.00169','0.00186','0.00117','0.00169','28.02171','28.02237','28.02167','28.02227','28.02077','28.02003','28.01995','28.02078','GradeC'",
                "'S023','0.00137','0.00155','0.00125','0.00153','0.00128','0.00144','0.00113','0.00169','28.02262','28.02340','28.02200','28.02272','28.02164','28.02068','28.02026','28.02082','GradeC'",
                "'S024','0.00127','0.00160','0.00151','0.00160','0.00109','0.00139','0.00110','0.00139','28.01994','28.02080','28.01934','28.01996','28.02013','28.01937','28.01831','28.01919','GradeB'",
                "'S025','0.00236','0.00174','0.00186','0.00209','0.00145','0.00110','0.00210','0.00170','28.02184','28.02217','28.02068','28.02149','28.02100','28.02032','28.01914','28.02029','GradeC'",
                "'S026','0.00105','0.00138','0.00106','0.00126','0.00117','0.00129','0.00095','0.00125','28.01930','28.01978','28.01920','28.01945','28.01860','28.01782','28.01747','28.01824','GradeC'",
                "'S027','0.00120','0.00153','0.00133','0.00145','0.00122','0.00123','0.00092','0.00163','28.02133','28.02157','28.02107','28.02129','28.02081','28.01985','28.01966','28.02063','GradeB'",
                "'S028','0.00252','0.00668','0.00347','0.00316','0.00674','0.00102','0.00145','0.00171','28.02138','28.02185','28.02058','28.02109','28.02124','28.02064','28.01956','28.02042','GradeC'",
                "'S030','0.00203','0.00141','0.00140','0.00143','0.00097','0.00094','0.00093','0.00103','28.01971','28.02019','28.01885','28.01951','28.01875','28.01834','28.01734','28.01823','GradeC'",
                "'S039','0.00191','0.00190','0.00175','0.00174','0.00121','0.00106','0.00122','0.00165','28.01863','28.02033','28.01993','28.02033','28.01576','28.01512','28.01518','28.01608','GradeC'",
                "'S041','0.00132','0.00147','0.00128','0.00159','0.00152','0.00171','0.00146','0.00212','28.01845','28.01889','28.01909','28.01907','28.01782','28.01700','28.01721','28.01850','GradeC'",
                "'S042','0.00122','0.00155','0.00133','0.00154','0.00154','0.00156','0.00115','0.00172','28.02198','28.02234','28.02198','28.02230','28.02150','28.02016','28.02036','28.02110','GradeC'",
                "'S043','0.00145','0.00182','0.00146','0.00173','0.00180','0.00171','0.00134','0.00184','28.01947','28.01981','28.01989','28.02027','28.01904','28.01849','28.01849','28.01969','GradeB'",
                "'S044','0.00260','0.00187','0.00171','0.00186','0.00226','0.00195','0.00126','0.00147','28.02244','28.02320','28.02328','28.02356','28.01735','28.01629','28.01595','28.01731','GradeC'",
                "'S046','0.00149','0.00485','0.00399','0.00175','0.00490','0.00120','0.00106','0.00103','28.01830','28.01828','28.01911','28.01928','28.01807','28.01725','28.01669','28.01781','GradeC'",
                "'S047','0.00166','0.00435','0.00418','0.00183','0.00130','0.00148','0.00124','0.00215','28.02005','28.02079','28.01981','28.02023','28.01690','28.01626','28.01502','28.01614','GradeC'",
                "'S048','0.00181','0.00211','0.00249','0.00268','0.00138','0.00128','0.00145','0.00159','28.01958','28.02046','28.01856','28.01926','28.02193','28.02083','28.01980','28.02093','GradeC'",
                "'S049','0.00154','0.00178','0.00147','0.00184','0.00157','0.00161','0.00137','0.00182','28.01947','28.01989','28.01943','28.01965','28.01922','28.01850','28.01792','28.01904','GradeC'",
                "'S050','0.00167','0.00191','0.00470','0.00178','0.00180','0.00130','0.00135','0.00180','28.01827','28.01861','28.01775','28.01799','28.01833','28.01773','28.01697','28.01805','GradeC'",
                "'S052','0.00261','0.00183','0.00456','0.00174','0.00152','0.00158','0.00318','0.00184','28.02269','28.02325','28.02195','28.02213','28.02261','28.02159','28.02083','28.02171','GradeC'",
                "'S054','0.00133','0.00154','0.00174','0.00165','0.00151','0.00058','0.00196','0.00252','28.01727','28.01763','28.01683','28.01751','28.02273','28.02211','28.02325','28.02426','GradeC'",
                "'S062','0.00083','0.00131','0.00107','0.00090','0.00122','0.00152','0.00135','0.00197','28.02108','28.02120','28.02206','28.02240','28.01968','28.01884','28.02031','28.02130','GradeC'",
                "'S063','0.00127','0.00082','0.00090','0.00088','0.00146','0.00167','0.00132','0.00202','28.01920','28.01936','28.01998','28.02014','28.01887','28.01801','28.01857','28.01975','GradeB'",
                "'S064','0.00122','0.00096','0.00091','0.00104','0.00122','0.00127','0.00089','0.00200','28.01986','28.01994','28.02126','28.02131','28.01888','28.01788','28.01959','28.02064','GradeC'",
                "'S066','0.00091','0.00090','0.00103','0.00092','0.00099','0.00104','0.00082','0.00122','28.01822','28.01872','28.01985','28.01962','28.01878','28.01756','28.01845','28.01996','GradeC'",
                "'S067','0.00107','0.00107','0.00114','0.00099','0.00104','0.00095','0.00108','0.00145','28.02028','28.02035','28.02052','28.02133','28.01952','28.01858','28.01928','28.02088','GradeB'",
                "'S068','0.00093','0.00096','0.00160','0.00090','0.00076','0.00073','0.00060','0.00151','28.02018','28.02032','28.02076','28.02096','28.01873','28.00982','28.01893','28.02025','GradeC'",
                "'S069','0.00125','0.00094','0.00101','0.00092','0.00085','0.00085','0.00085','0.00153','28.01865','28.01895','28.01971','28.01973','28.01743','28.01629','28.01719','28.01871','GradeC'",
                "'S070','0.00107','0.00100','0.00122','0.00113','0.00057','0.00065','0.00065','0.00112','28.02190','28.01973','28.02075','28.02045','28.02102','28.02108','28.02176','28.02172','GradeB'",
                "'S071','0.00099','0.00092','0.00107','0.00096','0.00064','0.00068','0.00051','0.00117','28.02216','28.02190','28.02290','28.02254','28.02073','28.02028','28.02071','28.02196','GradeC'",
                "'S072','0.00098','0.00092','0.00115','0.00113','0.00050','0.00042','0.00048','0.00101','28.02122','28.02148','28.02178','28.02212','28.01986','28.01850','28.02014','28.02136','GradeC'",
                "'S073','0.00104','0.00099','0.00116','0.01188','0.00053','0.00053','0.01190','0.00103','28.02061','28.02073','28.02097','28.02141','28.01961','28.01871','28.01952','28.02069','GradeC'",
                "'S074','0.00105','0.00099','0.00119','0.00109','0.00063','0.00056','0.00051','0.00069','28.01912','28.01954','28.02002','28.02028','28.01930','28.01860','28.01920','28.02042','GradeB'",
                "'S076','0.00074','0.00092','0.00100','0.00089','0.00062','0.00061','0.00050','0.00090','28.01992','28.01976','28.02042','28.02066','28.01940','28.01824','28.01876','28.02002','GradeB'",
                "'S079','0.00098','0.00085','0.00125','0.00113','0.00087','0.00081','0.00066','0.00078','28.02103','28.02091','28.02137','28.02117','28.01921','28.01819','28.01815','28.01917','GradeB'",
                "'S080','0.00145','0.00096','0.00155','0.00134','0.00050','0.00054','0.00051','0.00091','28.02123','28.02186','28.02137','28.02138','28.02084','28.02046','28.02042','28.02116','GradeB'",
                "'S081','0.00100','0.00110','0.00118','0.00124','0.00086','0.00095','0.00069','0.00099','28.02366','28.02374','28.02435','28.02430','28.02325','28.02229','28.02279','28.02399','GradeC'",
                "'S082','0.00111','0.00132','0.00142','0.00148','0.00070','0.00079','0.00070','0.00113','28.02074','28.02069','28.02103','28.02089','28.01948','28.01830','28.01912','28.02046','GradeB'",
                "'S083','0.00097','0.00107','0.00116','0.00111','0.00069','0.00079','0.00064','0.00095','28.02651','28.02667','28.02679','28.02695','28.02444','28.02378','28.02430','28.02520','GradeC'",
                "'S084','0.00118','0.00131','0.00185','0.00470','0.00098','0.00088','0.00072','0.00059','28.02340','28.02352','28.02406','28.02422','28.02302','28.02158','28.02178','28.02332','GradeC'",
                "'S085','0.00165','0.00195','0.00176','0.00179','0.00170','0.00190','0.00158','0.00201','28.02395','28.02442','28.02589','28.02621','28.02432','28.02350','28.02521','28.02632','GradeC'",
                "'S087 (78B)','0.00089','0.00111','0.00121','0.00113','0.00094','0.00092','0.00079','0.00148','28.02088','28.02102','28.02197','28.02218','28.02033','28.01925','28.02060','28.02139','GradeC'",
                "'S089','0.00333','0.00096','0.00158','0.00097','0.00117','0.00068','0.00115','0.00196','28.02295','28.02331','28.02416','28.02435','28.02073','28.02017','28.02134','28.02245','GradeC'",
                "'S090','0.00104','0.00123','0.00112','0.00123','0.00109','0.00111','0.00079','0.00119','28.02256','28.02270','28.02414','28.02432','28.02346','28.02237','28.02193','28.02384','GradeC'",
                "'S091','0.00109','0.00136','0.00243','0.00374','0.00049','0.00085','0.00041','0.00364','28.02339','28.02387','28.02433','28.02453','28.02284','28.02266','28.02346','28.02420','GradeC'",
                "'S094','0.00122','0.00106','0.00133','0.00131','0.00131','0.00130','0.00062','0.00132','28.02456','28.02396','28.02340','28.02538','28.02493','28.02189','28.02309','28.02637','GradeC'",
                "'S095','0.00160','0.00163','0.00154','0.00284','0.00089','0.00206','0.00120','0.00264','28.01704','28.01730','28.01732','28.01768','28.01930','28.01834','28.01953','28.02051','GradeC'",
                "'S096','0.00082','0.00077','0.00091','0.00079','0.00052','0.00057','0.00067','0.00125','28.02303','28.02295','28.02309','28.02317','28.02140','28.02082','28.02125','28.02220','GradeC'",
                "'S097','0.00101','0.00081','0.00336','0.00074','0.00076','0.00076','0.00057','0.00109','28.01977','28.02035','28.02039','28.02109','28.01917','28.01613','28.01918','28.02021','GradeC'",
                "'S098','0.00110','0.00212','0.00090','0.00418','0.00082','0.00203','0.00406','0.00091','28.01995','28.02033','28.02025','28.02009','28.01941','28.01861','28.01907','28.01997','GradeC'",
                "'S099','0.00129','0.00095','0.00125','0.00099','0.00077','0.00067','0.00062','0.00096','28.01910','28.01946','28.01970','28.01972','28.01668','28.01642','28.01542','28.01790','GradeC'",
                "'S100','0.00098','0.00138','0.00096','0.00115','0.00072','0.00085','0.00080','0.00148','28.02192','28.02212','28.02202','28.02210','28.02143','28.02050','28.02148','28.02253','GradeC'",
                "'S101','0.00122','0.00109','0.00099','0.00105','0.00068','0.00084','0.00069','0.00099','28.02158','28.02207','28.02159','28.02211','28.02095','28.02050','28.02024','28.02139','GradeC'",
                "'S102','0.00091','0.00105','0.00093','0.00101','0.00061','0.00072','0.00078','0.00127','28.02055','28.02089','28.02132','28.02155','28.01992','28.01932','28.01992','28.02100','GradeB'",
                "'S104','0.00181','0.00252','0.00101','0.00128','0.00088','0.00132','0.00069','0.00099','28.02062','28.02074','28.02118','28.02142','28.01915','28.01819','28.01691','28.01957','GradeC'",
                "'S108A','0.00146','0.00130','0.00121','0.00116','0.00111','0.00086','0.00105','0.00121','28.01686','28.01708','28.01776','28.01804','28.01644','28.01584','28.01628','28.01742','GradeC'",
                "'S112','0.00095','0.00124','0.00107','0.00127','0.00102','0.00130','0.00048','0.00100','28.01871','28.01870','28.01927','28.01965','28.01618','28.01512','28.01484','28.01616','GradeC'",
                "'S113','0.00112','0.00233','0.00170','0.00125','0.00112','0.00147','0.00126','0.00183','28.02192','28.02167','28.02285','28.02292','28.01862','28.01892','28.02011','28.01972','GradeC'",
                "'S114','0.00087','0.00120','0.00120','0.00124','0.00182','0.00174','0.00059','0.00106','28.02237','28.02211','28.02317','28.02327','28.01788','28.01682','28.01682','28.01773','GradeC'",
                "'S115','0.00093','0.00126','0.00104','0.00152','0.00100','0.00141','0.00111','0.00152','28.02036','28.02004','28.02110','28.02108','28.01920','28.01776','28.01838','28.01988','GradeC'",
                "'S116','0.00094','0.00129','0.00107','0.00117','0.00062','0.00112','0.00091','0.00126','28.02008','28.02016','28.02054','28.02052','28.01969','28.01837','28.01873','28.02009','GradeB'",
                "'S117','0.00083','0.00113','0.00102','0.00139','0.00073','0.00111','0.00101','0.00155','28.02194','28.02178','28.02230','28.02210','28.02066','28.01941','28.02023','28.02126','GradeC'",
                "'S119','0.00159','0.00200','0.00151','0.00183','0.00124','0.00150','0.00123','0.00139','28.02006','28.02017','28.02099','28.02099','28.01949','28.01865','28.01985','28.02081','GradeB'",
            };

            Assert.Equal(expected.WithDoubleQuotes(), stringList);
        }

    }
}
