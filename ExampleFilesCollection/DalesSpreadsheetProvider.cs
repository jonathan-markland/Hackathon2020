﻿using System.Collections.Generic;
using MCosmosClassLibrary;

namespace ExampleFilesCollection
{
    public static class DalesSpreadsheetProvider
    {
        private static DiscInfo data(
            string serialNo,
            double flatF, double flatE, double flatD, double flatG,
            double elh1, double erh1, double gfr1, double gbk1,
            double ef15lh, double ef15rh, double ef103lh, double ef103rh,
            double gd15fr, double gd15bk, double gd103fr, double gd103bk)
        {
            var metadata = new DiscMetadata { SerialNo = serialNo };
            var flatness = new FlatnessMeasurements { DatumF = flatF, DatumE = flatE, DatumD = flatD, DatumG = flatG };
            var parallel = new ParallelMeasurements { DatumELH1 = elh1, DatumERH1 = erh1, DatumGFR1 = gfr1, DatumGBK1 = gbk1 };
            var distance = new DistanceMeasurements
            {
                EtoFLeft1 = ef15lh,
                EtoFRight1 = ef15rh,
                EtoFLeft2 = ef103lh,
                EtoFRight2 = ef103rh,
                GtoDBack1 = gd15fr,
                GtoDFront1 = gd15bk,
                GtoDFront2 = gd103fr,
                GtoDBack2 = gd103bk
            };
            return new DiscInfo
            {
                Metadata = metadata,
                Flatness = flatness,
                Parallel = parallel,
                Distances = distance
            };
        }

        public static IEnumerable<DiscInfo> GroundAtStruder()
        {
            yield return data("005D", 0.002, 0.001, 0.001, 0.002, 0.000, 0.001, 0.000, 0.001, 28.017, 28.018, 28.018, 28.017, 28.017, 28.018, 28.017, 28.019);
            yield return data("006", 0.003, 0.001, 0.002, 0.002, 0.001, 0.001, 0.001, 0.001, 28.018, 28.018, 28.019, 28.019, 28.017, 28.018, 28.018, 28.019);
            yield return data("007", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.001, 0.002, 28.022, 28.023, 28.024, 28.024, 28.022, 28.023, 28.023, 28.025);
            yield return data("008", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.001, 0.001, 28.021, 28.021, 28.021, 28.022, 28.020, 28.021, 28.020, 28.022);
            yield return data("009", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.022, 28.023, 28.023, 28.024, 28.022, 28.023, 28.023, 28.024);
            yield return data("010", 0.002, 0.002, 0.002, 0.002, 0.001, 0.001, 0.001, 0.002, 28.023, 28.023, 28.023, 28.024, 28.023, 28.024, 28.024, 28.025);
            yield return data("011", 0.001, 0.002, 0.002, 0.001, 0.002, 0.002, 0.002, 0.003, 28.012, 28.012, 28.012, 28.013, 28.011, 28.012, 28.012, 28.013);
            yield return data("012", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.001, 0.002, 28.019, 28.019, 28.018, 28.019, 28.017, 28.018, 28.018, 28.019);
            yield return data("013", 0.001, 0.001, 0.001, 0.001, 0.002, 0.002, 0.002, 0.002, 28.021, 28.021, 28.021, 28.022, 28.021, 28.021, 28.021, 28.022);
            yield return data("014", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.002, 0.002, 28.022, 28.022, 28.023, 28.023, 28.022, 28.023, 28.022, 28.023);
            yield return data("015", 0.002, 0.001, 0.001, 0.001, 0.002, 0.002, 0.002, 0.002, 28.020, 28.020, 28.021, 28.021, 28.019, 28.020, 28.020, 28.021);
            yield return data("S009", 0.003, 0.003, 0.003, 0.003, 0.002, 0.001, 0.003, 0.001, 28.018, 28.025, 28.017, 28.026, 28.017, 28.024, 28.014, 28.014);
            yield return data("S010", 0.002, 0.002, 0.002, 0.002, 0.001, 0.001, 0.001, 0.001, 28.022, 28.023, 28.020, 28.021, 28.020, 28.021, 28.017, 28.019);
            yield return data("S011", 0.001, 0.002, 0.002, 0.002, 0.001, 0.001, 0.001, 0.001, 28.021, 28.022, 28.020, 28.021, 28.020, 28.021, 28.018, 28.019);
            yield return data("S014", 0.001, 0.001, 0.002, 0.001, 0.002, 0.002, 0.001, 0.002, 28.018, 28.019, 28.016, 28.019, 28.017, 28.018, 28.018, 28.019);
            yield return data("S015", 0.001, 0.002, 0.001, 0.001, 0.002, 0.002, 0.001, 0.002, 28.018, 28.019, 28.018, 28.019, 28.017, 28.017, 28.017, 28.017);
            yield return data("S016", 0.001, 0.001, 0.002, 0.001, 0.001, 0.001, 0.002, 0.002, 28.018, 28.019, 28.017, 28.018, 28.016, 28.017, 28.016, 28.017);
            yield return data("S017", 0.003, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.018, 28.019, 28.018, 28.018, 28.018, 28.019, 28.017, 28.018);
            yield return data("S019", 0.001, 0.002, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.017, 28.018, 28.016, 28.017, 28.019, 28.020, 28.018, 28.019);
            yield return data("S021", 0.002, 0.002, 0.001, 0.002, 0.001, 0.001, 0.001, 0.002, 28.019, 28.020, 28.019, 28.019, 28.019, 28.020, 28.019, 28.020);
            yield return data("S022", 0.001, 0.001, 0.001, 0.002, 0.002, 0.002, 0.001, 0.002, 28.022, 28.022, 28.022, 28.022, 28.020, 28.021, 28.020, 28.021);
            yield return data("S023", 0.001, 0.002, 0.001, 0.002, 0.001, 0.001, 0.001, 0.002, 28.023, 28.023, 28.022, 28.023, 28.021, 28.022, 28.020, 28.021);
            yield return data("S024", 0.001, 0.002, 0.002, 0.002, 0.001, 0.001, 0.001, 0.001, 28.020, 28.021, 28.019, 28.020, 28.019, 28.020, 28.018, 28.019);
            yield return data("S025", 0.002, 0.002, 0.002, 0.002, 0.001, 0.001, 0.002, 0.002, 28.022, 28.022, 28.021, 28.021, 28.020, 28.021, 28.019, 28.020);
            yield return data("S026", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.019, 28.020, 28.019, 28.019, 28.018, 28.019, 28.017, 28.018);
            yield return data("S027", 0.001, 0.002, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.021, 28.022, 28.021, 28.021, 28.020, 28.021, 28.020, 28.021);
            yield return data("S028", 0.003, 0.007, 0.003, 0.003, 0.007, 0.001, 0.001, 0.002, 28.021, 28.022, 28.021, 28.021, 28.021, 28.021, 28.020, 28.020);
            yield return data("S030", 0.002, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.020, 28.020, 28.019, 28.020, 28.018, 28.019, 28.017, 28.018);
            yield return data("S039", 0.002, 0.002, 0.002, 0.002, 0.001, 0.001, 0.001, 0.002, 28.019, 28.020, 28.020, 28.020, 28.015, 28.016, 28.015, 28.016);
            yield return data("S041", 0.001, 0.001, 0.001, 0.002, 0.002, 0.002, 0.001, 0.002, 28.018, 28.019, 28.019, 28.019, 28.017, 28.018, 28.017, 28.019);
            yield return data("S042", 0.001, 0.002, 0.001, 0.002, 0.002, 0.002, 0.001, 0.002, 28.022, 28.022, 28.022, 28.022, 28.020, 28.022, 28.020, 28.021);
            yield return data("S043", 0.001, 0.002, 0.001, 0.002, 0.002, 0.002, 0.001, 0.002, 28.019, 28.020, 28.020, 28.020, 28.018, 28.019, 28.018, 28.020);
            yield return data("S044", 0.003, 0.002, 0.002, 0.002, 0.002, 0.002, 0.001, 0.001, 28.022, 28.023, 28.023, 28.024, 28.016, 28.017, 28.016, 28.017);
            yield return data("S046", 0.001, 0.005, 0.004, 0.002, 0.005, 0.001, 0.001, 0.001, 28.018, 28.018, 28.019, 28.019, 28.017, 28.018, 28.017, 28.018);
            yield return data("S047", 0.002, 0.004, 0.004, 0.002, 0.001, 0.001, 0.001, 0.002, 28.020, 28.021, 28.020, 28.020, 28.016, 28.017, 28.015, 28.016);
            yield return data("S048", 0.002, 0.002, 0.002, 0.003, 0.001, 0.001, 0.001, 0.002, 28.020, 28.020, 28.019, 28.019, 28.021, 28.022, 28.020, 28.021);
            yield return data("S049", 0.002, 0.002, 0.001, 0.002, 0.002, 0.002, 0.001, 0.002, 28.019, 28.020, 28.019, 28.020, 28.019, 28.019, 28.018, 28.019);
            yield return data("S050", 0.002, 0.002, 0.005, 0.002, 0.002, 0.001, 0.001, 0.002, 28.018, 28.019, 28.018, 28.018, 28.018, 28.018, 28.017, 28.018);
            yield return data("S052", 0.003, 0.002, 0.005, 0.002, 0.002, 0.002, 0.003, 0.002, 28.023, 28.023, 28.022, 28.022, 28.022, 28.023, 28.021, 28.022);
            yield return data("S054", 0.001, 0.002, 0.002, 0.002, 0.002, 0.001, 0.002, 0.003, 28.017, 28.018, 28.017, 28.018, 28.022, 28.023, 28.023, 28.024);
            yield return data("S062", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.001, 0.002, 28.021, 28.021, 28.022, 28.022, 28.019, 28.020, 28.020, 28.021);
            yield return data("S063", 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 0.001, 0.002, 28.019, 28.019, 28.020, 28.020, 28.018, 28.019, 28.019, 28.020);
            yield return data("S064", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.020, 28.020, 28.021, 28.021, 28.018, 28.019, 28.020, 28.021);
            yield return data("S066", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.018, 28.019, 28.020, 28.020, 28.018, 28.019, 28.018, 28.020);
            yield return data("S067", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.020, 28.020, 28.021, 28.021, 28.019, 28.020, 28.019, 28.021);
            yield return data("S068", 0.001, 0.001, 0.002, 0.001, 0.001, 0.001, 0.001, 0.002, 28.020, 28.020, 28.021, 28.021, 28.010, 28.019, 28.019, 28.020);
            yield return data("S069", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.019, 28.019, 28.020, 28.020, 28.016, 28.017, 28.017, 28.019);
            yield return data("S070", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.022, 28.020, 28.021, 28.020, 28.021, 28.021, 28.022, 28.022);
            yield return data("S071", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.022, 28.022, 28.023, 28.023, 28.020, 28.021, 28.021, 28.022);
            yield return data("S072", 0.001, 0.001, 0.001, 0.001, 0.001, 0.000, 0.000, 0.001, 28.021, 28.021, 28.022, 28.022, 28.019, 28.020, 28.020, 28.021);
            yield return data("S073", 0.001, 0.001, 0.001, 0.012, 0.001, 0.001, 0.012, 0.001, 28.021, 28.021, 28.021, 28.021, 28.019, 28.020, 28.020, 28.021);
            yield return data("S074", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.019, 28.020, 28.020, 28.020, 28.019, 28.019, 28.019, 28.020);
            yield return data("S076", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.020, 28.020, 28.020, 28.021, 28.018, 28.019, 28.019, 28.020);
            yield return data("S079", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.021, 28.021, 28.021, 28.018, 28.019, 28.018, 28.019);
            yield return data("S080", 0.001, 0.001, 0.002, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.022, 28.021, 28.021, 28.020, 28.021, 28.020, 28.021);
            yield return data("S081", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.024, 28.024, 28.024, 28.024, 28.022, 28.023, 28.023, 28.024);
            yield return data("S082", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.021, 28.021, 28.021, 28.018, 28.019, 28.019, 28.020);
            yield return data("S083", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.027, 28.027, 28.027, 28.027, 28.024, 28.024, 28.024, 28.025);
            yield return data("S084", 0.001, 0.001, 0.002, 0.005, 0.001, 0.001, 0.001, 0.001, 28.023, 28.024, 28.024, 28.024, 28.022, 28.023, 28.022, 28.023);
            yield return data("S085", 0.002, 0.002, 0.002, 0.002, 0.002, 0.002, 0.002, 0.002, 28.024, 28.024, 28.026, 28.026, 28.024, 28.024, 28.025, 28.026);
            yield return data("S087 (78B)", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.021, 28.022, 28.022, 28.019, 28.020, 28.021, 28.021);
            yield return data("S089", 0.003, 0.001, 0.002, 0.001, 0.001, 0.001, 0.001, 0.002, 28.023, 28.023, 28.024, 28.024, 28.020, 28.021, 28.021, 28.022);
            yield return data("S090", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.023, 28.023, 28.024, 28.024, 28.022, 28.023, 28.022, 28.024);
            yield return data("S091", 0.001, 0.001, 0.002, 0.004, 0.000, 0.001, 0.000, 0.004, 28.023, 28.024, 28.024, 28.025, 28.023, 28.023, 28.023, 28.024);
            yield return data("S094", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.025, 28.024, 28.023, 28.025, 28.022, 28.025, 28.023, 28.026);
            yield return data("S095", 0.002, 0.002, 0.002, 0.003, 0.001, 0.002, 0.001, 0.003, 28.017, 28.017, 28.017, 28.018, 28.018, 28.019, 28.020, 28.021);
            yield return data("S096", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.023, 28.023, 28.023, 28.023, 28.021, 28.021, 28.021, 28.022);
            yield return data("S097", 0.001, 0.001, 0.003, 0.001, 0.001, 0.001, 0.001, 0.001, 28.020, 28.020, 28.020, 28.021, 28.016, 28.019, 28.019, 28.020);
            yield return data("S098", 0.001, 0.002, 0.001, 0.004, 0.001, 0.002, 0.004, 0.001, 28.020, 28.020, 28.020, 28.020, 28.019, 28.019, 28.019, 28.020);
            yield return data("S099", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.019, 28.019, 28.020, 28.020, 28.016, 28.017, 28.015, 28.018);
            yield return data("S100", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.022, 28.022, 28.022, 28.022, 28.021, 28.021, 28.021, 28.023);
            yield return data("S101", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.022, 28.022, 28.022, 28.022, 28.021, 28.021, 28.020, 28.021);
            yield return data("S102", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.021, 28.021, 28.022, 28.019, 28.020, 28.020, 28.021);
            yield return data("S104", 0.002, 0.003, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.021, 28.021, 28.021, 28.021, 28.018, 28.019, 28.017, 28.020);
            yield return data("S108A", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.017, 28.017, 28.018, 28.018, 28.016, 28.016, 28.016, 28.017);
            yield return data("S112", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.000, 0.001, 28.019, 28.019, 28.019, 28.020, 28.015, 28.016, 28.015, 28.016);
            yield return data("S113", 0.001, 0.002, 0.002, 0.001, 0.001, 0.001, 0.001, 0.002, 28.022, 28.022, 28.023, 28.023, 28.019, 28.019, 28.020, 28.020);
            yield return data("S114", 0.001, 0.001, 0.001, 0.001, 0.002, 0.002, 0.001, 0.001, 28.022, 28.022, 28.023, 28.023, 28.017, 28.018, 28.017, 28.018);
            yield return data("S115", 0.001, 0.001, 0.001, 0.002, 0.001, 0.001, 0.001, 0.002, 28.020, 28.020, 28.021, 28.021, 28.018, 28.019, 28.018, 28.020);
            yield return data("S116", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 28.020, 28.020, 28.021, 28.021, 28.018, 28.020, 28.019, 28.020);
            yield return data("S117", 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.001, 0.002, 28.022, 28.022, 28.022, 28.022, 28.019, 28.021, 28.020, 28.021);
            yield return data("S119", 0.002, 0.002, 0.002, 0.002, 0.001, 0.002, 0.001, 0.001, 28.020, 28.020, 28.021, 28.021, 28.019, 28.019, 28.020, 28.021);

        }

        public static IEnumerable<DiscInfo> SolihullNoGroovesOP20()
        {
            yield return data("042_OP20", 0.0009, 0.0009, 0.0008, 0.0010, 0.0008, 0.0006, 0.0011, 0.0007, 28.020, 28.019, 28.021, 28.020, 28.020, 28.020, 28.022, 28.020);
            yield return data("041_OP20", 0.0010, 0.0010, 0.0013, 0.0009, 0.0014, 0.0013, 0.0023, 0.0014, 28.020, 28.019, 28.021, 28.020, 28.020, 28.019, 28.021, 28.019);
            yield return data("031_OP20", 0.0008, 0.0009, 0.0029, 0.0009, 0.0010, 0.0010, 0.0009, 0.0007, 28.019, 28.019, 28.020, 28.020, 28.020, 28.019, 28.021, 28.020);
            yield return data("025_OP20", 0.0011, 0.0012, 0.0011, 0.0010, 0.0013, 0.0007, 0.0007, 0.0006, 28.020, 28.019, 28.020, 28.020, 28.022, 28.021, 28.023, 28.021);
            yield return data("029_OP20", 0.0010, 0.0008, 0.0012, 0.0013, 0.0008, 0.0012, 0.0011, 0.0009, 28.020, 28.021, 28.021, 28.022, 28.021, 28.021, 28.022, 28.021);
            yield return data("036_OP20", 0.0009, 0.0006, 0.0009, 0.0009, 0.0010, 0.0016, 0.0017, 0.0015, 28.022, 28.023, 28.024, 28.025, 28.022, 28.021, 28.023, 28.022);
            yield return data("027_OP20", 0.0013, 0.0014, 0.0016, 0.0010, 0.0009, 0.0009, 0.0014, 0.0032, 28.021, 28.021, 28.022, 28.022, 28.022, 28.022, 28.023, 28.025);
            yield return data("037_OP20", 0.0010, 0.0009, 0.0012, 0.0010, 0.0020, 0.0019, 0.0023, 0.0024, 28.023, 28.021, 28.024, 28.022, 28.020, 28.020, 28.022, 28.023);
            yield return data("033_OP20", 0.0008, 0.0009, 0.0010, 0.0010, 0.0009, 0.0006, 0.0006, 0.0011, 28.020, 28.019, 28.021, 28.020, 28.018, 28.018, 28.019, 28.019);
            yield return data("038_OP20", 0.0031, 0.0040, 0.0010, 0.0009, 0.0029, 0.0019, 0.0018, 0.0016, 28.020, 28.019, 28.021, 28.020, 28.019, 28.019, 28.021, 28.020);
            yield return data("078_OP20", 0.0011, 0.0008, 0.0016, 0.0011, 0.0005, 0.0007, 0.0005, 0.0005, 28.019, 28.019, 28.018, 28.019, 28.019, 28.018, 28.018, 28.018);
            yield return data("070_OP20", 0.0010, 0.0011, 0.0011, 0.0013, 0.0006, 0.0007, 0.0004, 0.0007, 28.020, 28.019, 28.019, 28.019, 28.019, 28.019, 28.019, 28.018);
            yield return data("065_OP20", 0.0014, 0.0014, 0.0013, 0.0016, 0.0005, 0.0004, 0.0008, 0.0005, 28.020, 28.020, 28.021, 28.020, 28.019, 28.019, 28.020, 28.019);
            yield return data("075_OP20", 0.0014, 0.0009, 0.0013, 0.0013, 0.0006, 0.0006, 0.0006, 0.0005, 28.021, 28.020, 28.020, 28.021, 28.019, 28.019, 28.020, 28.019);
            yield return data("053_OP20", 0.0008, 0.0007, 0.0009, 0.0009, 0.0003, 0.0008, 0.0000, 0.0003, 28.017, 28.018, 28.017, 28.019, 28.017, 28.016, 28.017, 28.016);
            yield return data("056_OP20", 0.0008, 0.0009, 0.0008, 0.0008, 0.0006, 0.0005, 0.0006, 0.0005, 28.018, 28.019, 28.018, 28.019, 28.018, 28.017, 28.018, 28.017);
            yield return data("054_OP20", 0.0011, 0.0008, 0.0017, 0.0007, 0.0007, 0.0008, 0.0006, 0.0005, 28.019, 28.020, 28.018, 28.020, 28.019, 28.018, 28.019, 28.018);
            yield return data("073_OP20", 0.0013, 0.0009, 0.0010, 0.0013, 0.0027, 0.0018, 0.0008, 0.0010, 28.020, 28.019, 28.018, 28.018, 28.018, 28.017, 28.018, 28.017);
            yield return data("068_OP20", 0.0012, 0.0020, 0.0013, 0.0013, 0.0005, 0.0020, 0.0006, 0.0004, 28.021, 28.020, 28.022, 28.021, 28.019, 28.020, 28.020, 28.020);
            yield return data("064_OP20", 0.0016, 0.0018, 0.0017, 0.0021, 0.0009, 0.0005, 0.0006, 0.0005, 28.021, 28.021, 28.021, 28.021, 28.020, 28.020, 28.021, 28.021);
            yield return data("049_OP20", 0.0012, 0.0012, 0.0013, 0.0014, 0.0018, 0.0013, 0.0019, 0.0021, 28.021, 28.021, 28.022, 28.021, 28.021, 28.021, 28.022, 28.022);
            yield return data("026_OP20", 0.0015, 0.0010, 0.0011, 0.0012, 0.0007, 0.0013, 0.0011, 0.0010, 28.021, 28.022, 28.021, 28.022, 28.022, 28.021, 28.022, 28.022);
            yield return data("060_OP20", 0.0009, 0.0009, 0.0010, 0.0008, 0.0007, 0.0004, 0.0007, 0.0004, 28.021, 28.021, 28.021, 28.022, 28.021, 28.022, 28.021, 28.023);
            yield return data("040_OP20", 0.0010, 0.0009, 0.0008, 0.0009, 0.0011, 0.0014, 0.0016, 0.0022, 28.022, 28.022, 28.022, 28.023, 28.020, 28.021, 28.022, 28.023);
            yield return data("080_OP20", 0.0013, 0.0013, 0.0014, 0.0012, 0.0004, 0.0003, 0.0006, 0.0003, 28.022, 28.022, 28.021, 28.021, 28.021, 28.021, 28.020, 28.020);
            yield return data("079_OP20", 0.0012, 0.0020, 0.0010, 0.0010, 0.0005, 0.0005, 0.0004, 0.0004, 28.022, 28.022, 28.022, 28.022, 28.022, 28.021, 28.022, 28.021);
            yield return data("077_OP20", 0.0011, 0.0012, 0.0013, 0.0014, 0.0007, 0.0004, 0.0004, 0.0004, 28.022, 28.022, 28.023, 28.022, 28.020, 28.021, 28.020, 28.020);
            yield return data("072_OP20", 0.0011, 0.0013, 0.0013, 0.0013, 0.0006, 0.0011, 0.0010, 0.0007, 28.022, 28.022, 28.022, 28.021, 28.021, 28.021, 28.021, 28.021);
            yield return data("062_OP20", 0.0010, 0.0008, 0.0008, 0.0009, 0.0009, 0.0004, 0.0003, 0.0003, 28.022, 28.024, 28.022, 28.023, 28.022, 28.021, 28.021, 28.021);
            yield return data("058_OP20", 0.0010, 0.0009, 0.0009, 0.0010, 0.0007, 0.0004, 0.0004, 0.0005, 28.023, 28.024, 28.023, 28.024, 28.021, 28.021, 28.022, 28.022);
            yield return data("061_OP20", 0.0010, 0.0009, 0.0010, 0.0010, 0.0006, 0.0005, 0.0011, 0.0005, 28.020, 28.021, 28.020, 28.021, 28.019, 28.021, 28.019, 28.021);
            yield return data("074_OP20", 0.0011, 0.0017, 0.0011, 0.0010, 0.0006, 0.0013, 0.0004, 0.0011, 28.020, 28.021, 28.021, 28.021, 28.020, 28.021, 28.021, 28.021);
            yield return data("059_OP20", 0.0010, 0.0009, 0.0007, 0.0011, 0.0004, 0.0004, 0.0005, 0.0009, 28.018, 28.018, 28.019, 28.018, 28.019, 28.018, 28.020, 28.018);
            yield return data("032_OP20", 0.0010, 0.0009, 0.0008, 0.0010, 0.0007, 0.0007, 0.0008, 0.0006, 28.018, 28.018, 28.019, 28.018, 28.020, 28.019, 28.021, 28.020);
            yield return data("057_OP20", 0.0018, 0.0011, 0.0013, 0.0011, 0.0003, 0.0005, 0.0004, 0.0005, 28.020, 28.019, 28.021, 28.020, 28.018, 28.019, 28.019, 28.019);
            yield return data("063_OP20", 0.0028, 0.0008, 0.0009, 0.0009, 0.0007, 0.0003, 0.0004, 0.0006, 28.020, 28.021, 28.020, 28.021, 28.020, 28.019, 28.021, 28.019);
            yield return data("045_OP20", 0.0008, 0.0007, 0.0007, 0.0009, 0.0023, 0.0021, 0.0025, 0.0028, 28.021, 28.020, 28.023, 28.021, 28.018, 28.019, 28.020, 28.021);
            yield return data("035_OP20", 0.0028, 0.0009, 0.0009, 0.0008, 0.0008, 0.0005, 0.0007, 0.0009, 28.023, 28.022, 28.023, 28.022, 28.020, 28.020, 28.021, 28.021);
            yield return data("069_OP20", 0.0012, 0.0012, 0.0034, 0.0011, 0.0007, 0.0011, 0.0006, 0.0017, 28.018, 28.018, 28.018, 28.017, 28.017, 28.017, 28.017, 28.017);
            yield return data("039_OP20", 0.0008, 0.0008, 0.0010, 0.0018, 0.0004, 0.0010, 0.0011, 0.0014, 28.019, 28.020, 28.018, 28.020, 28.016, 28.016, 28.017, 28.016);
            yield return data("066_OP20", 0.0014, 0.0012, 0.0018, 0.0016, 0.0005, 0.0006, 0.0005, 0.0010, 28.028, 28.026, 28.028, 28.027, 28.026, 28.026, 28.026, 28.026);
            yield return data("067_OP20", 0.0013, 0.0015, 0.0015, 0.0012, 0.0008, 0.0005, 0.0004, 0.0007, 28.014, 28.014, 28.015, 28.015, 28.014, 28.014, 28.015, 28.015);
            yield return data("050_OP20", 0.0015, 0.0013, 0.0013, 0.0015, 0.0022, 0.0026, 0.0021, 0.0029, 28.017, 28.018, 28.020, 28.020, 28.016, 28.019, 28.020, 28.021);
            yield return data("071_OP20", 0.0010, 0.0012, 0.0011, 0.0011, 0.0007, 0.0013, 0.0011, 0.0008, 28.020, 28.021, 28.020, 28.019, 28.019, 28.020, 28.019, 28.019);
            yield return data("052_OP20", 0.0008, 0.0009, 0.0011, 0.0009, 0.0017, 0.0014, 0.0019, 0.0021, 28.018, 28.016, 28.018, 28.017, 28.015, 28.015, 28.016, 28.017);
            yield return data("030_OP20", 0.0011, 0.0009, 0.0009, 0.0012, 0.0007, 0.0006, 0.0014, 0.0009, 28.021, 28.020, 28.022, 28.021, 28.025, 28.023, 28.026, 28.023);
            yield return data("043_OP20", 0.0008, 0.0008, 0.0007, 0.0007, 0.0004, 0.0006, 0.0004, 0.0006, 28.022, 28.022, 28.022, 28.023, 28.023, 28.024, 28.024, 28.025);
            yield return data("028_OP20", 0.0009, 0.0010, 0.0011, 0.0013, 0.0009, 0.0007, 0.0008, 0.0010, 28.024, 28.023, 28.025, 28.024, 28.023, 28.024, 28.024, 28.025);
            yield return data("051_OP20", 0.0013, 0.0014, 0.0012, 0.0013, 0.0017, 0.0017, 0.0017, 0.0012, 28.016, 28.016, 28.017, 28.016, 28.018, 28.016, 28.019, 28.018);
            yield return data("081_OP20", 0.0019, 0.0018, 0.0016, 0.0019, 0.0004, 0.0005, 0.0004, 0.0005, 28.017, 28.017, 28.016, 28.015, 28.017, 28.017, 28.017, 28.016);
            yield return data("044_OP20", 0.0010, 0.0021, 0.0009, 0.0009, 0.0020, 0.0025, 0.0017, 0.0024, 28.018, 28.018, 28.020, 28.020, 28.019, 28.019, 28.020, 28.021);

        }
    }
}
