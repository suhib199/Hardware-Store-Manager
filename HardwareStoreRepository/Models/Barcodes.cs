﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Models
{
    public  class Barcodes
    {
        [Key]
        [Column("BarcodeId")]
        public int Id { get; set; }
        public string BarCodeNumber{ get; set;}
        public virtual Product Product  { get; set;}

    }
}
