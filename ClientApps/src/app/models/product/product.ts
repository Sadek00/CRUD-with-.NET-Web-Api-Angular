export interface Product {
    id: number;
  categoryId: number | null;
  categoryName: string;
  unitName: string;
  name: string;
  code: string;
  parentCode: string | null;
  productBarcode: string;
  description: string;
  brandName: string;
  sizeName: string;
  colorName: string;
  modelName: string | null;
  variantName: string | null;
  oldPrice: number;
  price: number;
  costPrice: number;
  stock: number;
  totalPurchase: number;
  lastPurchaseDate: string;
  lastPurchaseSupplier: string;
  totalSales: number;
  lastSalesDate: string;
  lastSalesCustomer: string;
  type: string;
  status: string;
  commissionAmount: number;
  commissionPer: number;
  pctn: number;
  }
  