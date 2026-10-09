using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;



namespace Hrms.Lite.Infrastructure.Helpers
{
    public class Helpers : IHelpers
    {
        
        public async Task<string> GetFilterValues(Filter filter)
        {
            string FilterValues = "";
            if (filter.Basic_Filter != null)
            {

                if (filter.Basic_Filter.Designation.Code != null)
                {
                    FilterValues = FilterValues + "Designation: " + filter.Basic_Filter.Designation.Name;
                }
                if (filter.Basic_Filter.Department.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Department: " + filter.Basic_Filter.Department.Name;
                    else
                        FilterValues = FilterValues + ", " + "Department: " + filter.Basic_Filter.Department.Name;
                }
                if (filter.Basic_Filter.Location.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Location: " + filter.Basic_Filter.Location.Name;
                    else
                        FilterValues = FilterValues + ", " + "Location: " + filter.Basic_Filter.Location.Name;
                }
                if (filter.Basic_Filter.Category.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Category: " + filter.Basic_Filter.Category.Name;
                    else
                        FilterValues = FilterValues + ", " + "Category: " + filter.Basic_Filter.Category.Name;
                }
                if (filter.Basic_Filter.Grade.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Grade: " + filter.Basic_Filter.Grade.Name;
                    else
                        FilterValues = FilterValues + ", " + "Grade: " + filter.Basic_Filter.Grade.Name;
                }
                if (filter.Basic_Filter.PayrollGroup.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "PayrollGroup: " + filter.Basic_Filter.PayrollGroup.Name;
                    else
                        FilterValues = FilterValues + ", " + "PayrollGroup: " + filter.Basic_Filter.PayrollGroup.Name;
                }
            }
            if (filter.Optional_Filter != null)
            {

                if (filter.Optional_Filter.Division.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Division: " + filter.Optional_Filter.Division.Name;
                    else
                        FilterValues = FilterValues + ", " + "Division: " + filter.Optional_Filter.Division.Name;
                }
                if (filter.Optional_Filter.Section.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Section: " + filter.Optional_Filter.Section.Name;
                    else
                        FilterValues = FilterValues + ", " + "Section: " + filter.Optional_Filter.Section.Name;
                }
                if (filter.Optional_Filter.EmploymentType.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Employment Type: " + filter.Optional_Filter.EmploymentType.Name;
                    else
                        FilterValues = FilterValues + ", " + "Employment Type: " + filter.Optional_Filter.EmploymentType.Name;
                }
                if (filter.Optional_Filter.WorkLocation.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Work Location: " + filter.Optional_Filter.WorkLocation.Name;
                    else
                        FilterValues = FilterValues + ", " + "Work Location: " + filter.Optional_Filter.WorkLocation.Name;
                }
                if (filter.Optional_Filter.WageGradeMaster.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Wage Grade: " + filter.Optional_Filter.WageGradeMaster.Name;
                    else
                        FilterValues = FilterValues + ", " + "Wage Grade: " + filter.Optional_Filter.WageGradeMaster.Name;
                }
                //if (filter.Optional_Filter.EmploymentStatus.Code != 0)
                //{
                //    if (FilterValues == "")
                //        FilterValues = FilterValues + "Employment Status: " + filter.Optional_Filter.EmploymentStatus.Name;
                //    else
                //        FilterValues = FilterValues + ", " + "Employment Status: " + filter.Optional_Filter.EmploymentStatus.Name;
                //}
                if (filter.Optional_Filter.Gender.Code != null)
                {
                    if (FilterValues == "")
                        FilterValues = FilterValues + "Gender: " + filter.Optional_Filter.Gender.Name;
                    else
                        FilterValues = FilterValues + ", " + "Gender: " + filter.Optional_Filter.Gender.Name;
                }
            }
            return FilterValues;
        }
        public async Task<string> GetFilterValuesMultiSelect(FilterMultiSelect filter)
        {
            string FilterValues = "";
            if (filter.Basic_Filter != null)
            {

                if (filter.Basic_Filter.Designation_Code != null && filter.Basic_Filter.Designation_Code.Length >0)
                {
                    for(int i=0;i< filter.Basic_Filter.Designation_Code.Length;i++)
                        FilterValues = FilterValues + "Designation: " + filter.Basic_Filter.Designation_Name[i];
                }
                if (filter.Basic_Filter.Department_Code != null && filter.Basic_Filter.Department_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Basic_Filter.Department_Code.Length; i++)
                    { 
                       if (FilterValues == "")
                        FilterValues = FilterValues + "Department: " + filter.Basic_Filter.Department_Name[i];
                       else
                        FilterValues = FilterValues + ", " + "Department: " + filter.Basic_Filter.Department_Name[i];
                    }
                }
                if (filter.Basic_Filter.Location_Code != null && filter.Basic_Filter.Location_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Basic_Filter.Location_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Location: " + filter.Basic_Filter.Location_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Location: " + filter.Basic_Filter.Location_Name[i];
                    }
                }
                if (filter.Basic_Filter.Category_Code != null && filter.Basic_Filter.Category_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Basic_Filter.Category_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Category: " + filter.Basic_Filter.Category_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Category: " + filter.Basic_Filter.Category_Name[i];
                    }
                }
                if (filter.Basic_Filter.Grade_Code != null && filter.Basic_Filter.Grade_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Basic_Filter.Grade_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Grade: " + filter.Basic_Filter.Grade_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Grade: " + filter.Basic_Filter.Grade_Name[i];
                    }
                }
                if (filter.Basic_Filter.PayrollGroup_Code != null && filter.Basic_Filter.PayrollGroup_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Basic_Filter.PayrollGroup_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "PayrollGroup: " + filter.Basic_Filter.PayrollGroup_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "PayrollGroup: " + filter.Basic_Filter.PayrollGroup_Name[i];
                    }
                }
            }
            if (filter.Optional_Filter != null)
            {

                if (filter.Optional_Filter.Division_Code != null && filter.Optional_Filter.Division_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.Division_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Division: " + filter.Optional_Filter.Division_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Division: " + filter.Optional_Filter.Division_Name[i];
                    }
                }
                if (filter.Optional_Filter.Section_Code != null && filter.Optional_Filter.Section_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.Section_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Section: " + filter.Optional_Filter.Section_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Section: " + filter.Optional_Filter.Section_Name[i];
                    }
                }
                if (filter.Optional_Filter.EmploymentType_Code != null && filter.Optional_Filter.EmploymentType_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.EmploymentType_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Employment Type: " + filter.Optional_Filter.EmploymentType_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Employment Type: " + filter.Optional_Filter.EmploymentType_Name[i];
                    }
                }
                if (filter.Optional_Filter.WorkLocation_Code != null && filter.Optional_Filter.WorkLocation_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.WorkLocation_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Work Location: " + filter.Optional_Filter.WorkLocation_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Work Location: " + filter.Optional_Filter.WorkLocation_Name[i];
                    }
                }
                if (filter.Optional_Filter.WageGradeMaster_Code != null && filter.Optional_Filter.WageGradeMaster_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.WageGradeMaster_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Wage Grade: " + filter.Optional_Filter.WageGradeMaster_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Wage Grade: " + filter.Optional_Filter.WageGradeMaster_Name[i];
                    }
                }
                //if (filter.Optional_Filter.EmploymentStatus.Code != 0)
                //{
                //    if (FilterValues == "")
                //        FilterValues = FilterValues + "Employment Status: " + filter.Optional_Filter.EmploymentStatus.Name;
                //    else
                //        FilterValues = FilterValues + ", " + "Employment Status: " + filter.Optional_Filter.EmploymentStatus.Name;
                //}
                if (filter.Optional_Filter.Gender_Code != null && filter.Optional_Filter.Gender_Code.Length > 0)
                {
                    for (int i = 0; i < filter.Optional_Filter.Gender_Code.Length; i++)
                    {
                        if (FilterValues == "")
                            FilterValues = FilterValues + "Gender: " + filter.Optional_Filter.Gender_Name[i];
                        else
                            FilterValues = FilterValues + ", " + "Gender: " + filter.Optional_Filter.Gender_Name[i];
                    }
                }
            }
            return FilterValues;
        }

        public async Task<byte[]> CompressFile(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                using (var compressedStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(compressedStream, CompressionMode.Compress))
                    {
                        gzip.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }
                    return compressedStream.ToArray();
                }
            }
        }

        public async Task<byte[]> DecompressFile(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                await gzip.CopyToAsync(output);
                return output.ToArray();
            }
        }

        public async Task<byte[]> DecompressIfNeeded(byte[] data)
        {
            if (data == null || data.Length == 0)
                return data;

            if (IsGZip(data))
                return await DecompressFile(data);

            return data; // Not compressed
        }
        private bool IsGZip(byte[] data)
        {
            return data.Length > 2 &&
                   data[0] == 0x1F &&
                   data[1] == 0x8B;
        }
        public async Task<string> GetCurrentDate()
        {
            string currDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            return currDateTime;
        }
    }
}
