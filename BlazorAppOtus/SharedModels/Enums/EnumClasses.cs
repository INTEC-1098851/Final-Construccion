using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string ThirdParty = "3rdPartyService";
        public const string EmailValidation = "EmailValidation";
    }
    public enum PhoneType
    {
        Celular = 1,
        Casa = 2,
        Trabajo = 3,
        Otro = 4
    }
    public enum DocumentType
    {
        Cedula = 1,
        RNC = 2
    }
    public enum DiscountTypeC2K
    {
        POS = 0,
        Porcentaje = 1,
        MontoDescuento = 2
    }
    public enum Source
    {
        Mobile = 1,
        Web = 2
    }
    public enum AddressType
    {
        Casa = 1,
        Apartamento = 2,
        Trabajo = 3,
        Otros = 4,
        
    }
    public enum OrderStatus
    {
        //EnEspera=1,
        Cancelada=1,
        Recibida = 2,
        EnCocina= 3,
        EnCamino= 4,
        Entregado = 5,
        EnDespachadorC2K = 6,
        EnCocinaC2K = 7,
        FallidaC2K = 8,
        RetenidaPorTotal = 9,
        RetenidaPorPrecioProducto= 10,
        EnCocinaVisor = 15,
        EnCaminoVisor = 16
        //EnEspera = 9,
    }
    public enum OrderMode
    {
        Delivery = 1,
        TakeOut = 2,
        TrattoriaDelivery = 3,
        TrattoriaTakeOut = 4
    }
    public enum OrderType
    {
        Delivery = 1,
        TakeOut = 2
    }
    //public enum OrderType
    //{
    //    Delivery = 3,
    //    TakeOut = 4
    //}
    public enum RowType
    {
        Parent = 1,
        Child = 2
    }
    public enum TenderType
    {
        Efectivo = 1,
        CC = 2,
        Cheque = 3
    }
    public enum WeekDay
    {
        Domingo = 0,
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6
        //Feriado = 7
    }

    public enum Status
    {
        Inactivo= 0,
        Activo = 1,
        Eliminado = 2,
        SuspendidoMicros = 3,
        InactivoPorDependencia = 4,
        EliminadoPorDependencia = 5
    }
    public enum PriceLevel{
        Delivery=1
    }

    public enum ProductType
    {
        Padre=1,
        Hijo=2
    }

    public enum DiscountType
    {
        MontoFijo = 1,
        Porcentaje = 2,
        ItemGratis = 3
    }

    public enum ReceiverType
    {
        Correo = 1,
        Dominio = 2,
        Publico = 3,
        Lista = 4
    }

    public enum DiscountReason
    {
        PrimeraOrden=1,
        ProximaOrden=2,
        OrdenReferida=3,
        Consumo=4,
        B2B = 5,
        Coupon = 6,
        PromoCode = 7
    }

    public enum ApprovalStatus
    {
        PendienteEnviar= 1,
        PendienteAprobar = 2,
        Rechazado = 3,
        Aprobado = 4,
        Descartado = 5
    }

    public enum CollectionType
    {
        TipoMasa = 1,
        Oferta= 2,
        Menu = 3,
        Recomendacion = 4
    }
   
    public enum Brands
    {
        Pizzarelli = 1,
        Trattoria=2
    }
    //public enum ProductSubCategory
    //{
    //    ToppingPizza = 105
    //}
    public static class ProductSubCategory{
        public const string ToppingPizza = "Topping Pizza";
        public const string DeliveryFee = "Delivery Fee";
        public const string DeliveryFeeCompartido = "Delivery Fee Compartido";
    }

    public static class C2KStatus
    {
        public const string RECEIVED = "RECEIVED";
        public const string ONHOLD = "ON HOLD";
        public const string ONKITCHEN = "ON KITCHEN";
        public const string ONDISPATCHER = "ON DISPATCHER";
        public const string ONROUTE = "ON ROUTE";
        public const string DELIVERED= "DELIVERED";
        public const string CANCELLED= "RECEIVED-CANCELLED";
    }

  
    public enum Size
    {
        ArmaTuPizzaPiccoliza= 4
    }
    public enum RestrictionType
    {
        Producto=1,
        Condimento=2
    }
}
