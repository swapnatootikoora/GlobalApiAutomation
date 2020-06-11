using System;
using System.Collections.Generic;
using Common.Models;

namespace Common.TestData
{
    public static class DataClass
    {
        static string environment = Environment.GetEnvironmentVariable("RUNTIME_ENVIRONMENT") ?? "int";
        public class AgencyData
        {
            public Agency Agency;
            public List<Contact> AgencyContact;
            public List<ClientData> ClientData;
        }

        public class ClientData
        {
            public Client Client;
            public List<Contact> Contact;
            public List<BrandData> BrandData;
        }

        public class BrandData
        {
            public Brand Brand;
            public List<Product> Product;
        }

        public static AgencyData Agency1 { get; }
        public static AgencyData Agency2 { get; }
        public static AgencyData AgencyWithDisabledClientBrandAndProduct { get; }
        public static AgencyData DisabledAgency { get; }
        public static AgencyData AgencyWithDisabledClient { get; }
        public static AgencyData AgencyWithDisabledContact { get; }
        public static ClientData DirectClientOnly { get; }
        public static ClientData ClientWithDisabledContact { get; }
        public static ClientData ClientWithMultipleBrandsAndProducts { get; }
        public static List<People> OwnerOnly { get; }
        public static List<People> OwnerAndPlanner { get; }
        public static List<People> PlannerOnly { get; }
        public static List<People> DisabledOwnerAndPlanner { get; }
        public static Currency AUSCurrency { get; }
        public static Currency USCurrency { get; }
        
        static DataClass()
        {
            Agency1 = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "56171ce0-4be6-44ac-8379-aa9901188605",
                    Name = "Mind Works Marketing Co Limited",
                    CustomerReferenceNumber = "C1350197",
                    externalId = null,
                    IsDisabled = false
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "9d75e510-8d39-4cfc-8b33-aa9901188620",
                        Firstname = "Michelle",
                        Lastname = "Leggatt",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                    },
                    new Contact
                    {
                        Id = "b5f731aa-6fc3-431e-9f27-aa9901188620",
                        Firstname = "Emma",
                        Lastname = "Harbison",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                    }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "3c833e81-7d4a-457f-b72a-aa9901188615",
                            Name = "THE MARLANDS SHOPPING CENTRE",
                            CustomerReferenceNumber = "C1410078",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 3,
                            AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "c5c0d298-01a3-40cc-ac00-aa990118861d",
                                Firstname = "Deepeka",
                                Lastname = "Dayal",
                                IsDisabled = false,
                                ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615",
                                AgencyId = null
                            },
                            new Contact
                            {
                                Id = "6a1c7121-a2e9-46c8-9cd8-aa990118861d",
                                Firstname = "Tim",
                                Lastname = "Keeping",
                                IsDisabled = false,
                                ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "6c60d5d7-c12a-45f8-acc8-aa9901188618",
                                    Name = "The Marlands Shopping Centre",
                                    IsDisabled = false,
                                    ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "2ad03fa0-af9a-4a3b-9f82-aa990118861a",
                                        Name = "The Marlands Shopping Centre",
                                        IsDisabled = false,
                                        BrandId = "6c60d5d7-c12a-45f8-acc8-aa9901188618"
                                    }
                                }
                            }
                        }
                    },
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "40e67da3-20a7-49ac-b37d-aa9901188615",
                            Name = "CASCADES SHOPPING MALL (PO1 4RL) - SEE  C1393822",
                            CustomerReferenceNumber = "C1385576",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 3,
                            AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "ed4aace1-9961-4680-80a5-aa990118861d",
                                Firstname = "Rhoda",
                                Lastname = "Joseph",
                                IsDisabled = false,
                                ClientId = "40e67da3-20a7-49ac-b37d-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "6d2f6472-fa47-4098-b066-aa9901188618",
                                    Name = "Cascades Shopping Mall",
                                    IsDisabled = false,
                                    ClientId = "40e67da3-20a7-49ac-b37d-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "eae1a62b-159e-441b-b8c7-aa990118861a",
                                        Name = "Cascades Shopping Mall",
                                        IsDisabled = false,
                                        BrandId = "6d2f6472-fa47-4098-b066-aa9901188618"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            Agency2 = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "ccf92609-8663-4edd-9635-aa9901188605",
                    Name = "PENNA PLC",
                    CustomerReferenceNumber = "C1393595",
                    externalId = null,
                    IsDisabled = false
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "9cfcc2c6-9603-47ee-ae50-aa9901188620",
                        Firstname = "0",
                        Lastname = "0",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "ccf92609-8663-4edd-9635-aa9901188605"
                    },
                    new Contact
                    {
                        Id = "8a7f4f2a-6370-47d9-b016-aa9901188620",
                        Firstname = "Lauren",
                        Lastname = "Mcloughlin",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "ccf92609-8663-4edd-9635-aa9901188605"
                     }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "9005aa22-c7ef-4ba4-8458-aa9901188615",
                            Name = "DEBENHAMS (RETAIL HEAD OFFICE LONDON)",
                            CustomerReferenceNumber = "C1339594",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 2,
                            AgencyId = "ccf92609-8663-4edd-9635-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "b2d39983-27f1-45b3-a3e7-aa990118861d",
                                Firstname = "CAROLINE",
                                Lastname = "WEBB",
                                IsDisabled = false,
                                ClientId = "9005aa22-c7ef-4ba4-8458-aa9901188615",
                                AgencyId = null
                            },
                            new Contact
                            {
                                Id = "2aa41236-6082-4907-8377-aa990118861d",
                                Firstname = "Jane",
                                Lastname = "Exon",
                                IsDisabled = false,
                                ClientId = "9005aa22-c7ef-4ba4-8458-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "cd780ade-d9e7-452c-8905-aa9901188618",
                                    Name = "Big Summer Event",
                                    IsDisabled = false,
                                    ClientId = "9005aa22-c7ef-4ba4-8458-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "860c0ec2-dfa4-43c8-84ed-aa990118861b",
                                        Name = "Sale",
                                        IsDisabled = false,
                                        BrandId = "cd780ade-d9e7-452c-8905-aa9901188618"
                                    }
                                }
                            },
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "e0ad0989-120a-4a9f-ac4e-aa9901188618",
                                    Name = "Debenhams",
                                    IsDisabled = false,
                                    ClientId = "9005aa22-c7ef-4ba4-8458-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "72d216e3-646b-499c-90ed-aa990118861a",
                                        Name = "Debenhams September Spectacular",
                                        IsDisabled = false,
                                        BrandId = "e0ad0989-120a-4a9f-ac4e-aa9901188618"
                                    },
                                    new Product
                                    {
                                        Id = "aab57789-7615-469a-ae37-aa990118861a",
                                        Name = "Beauty Reward Club Card",
                                        IsDisabled = false,
                                        BrandId = "e0ad0989-120a-4a9f-ac4e-aa9901188618"
                                    }
                                }
                            }
                        }
                    },
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "de706df0-b394-43f5-9dc7-aa9901188615",
                            Name = "TELFORD & WREKIN COUNCIL (INDIRECT)",
                            CustomerReferenceNumber = "C1393594",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 2,
                            AgencyId = "ccf92609-8663-4edd-9635-aa9901188605"
                        },
                        Contact = null,
                        BrandData = null
                    }
                }
            };


            AgencyWithDisabledClientBrandAndProduct = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "f230dc0c-3422-491a-889e-aa9901188605",
                    Name = "the7stars",
                    CustomerReferenceNumber = "C1391750",
                    externalId = null,
                    IsDisabled = false
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "416fb665-42f6-4418-9e1c-aa9901188620",
                        Firstname = "Jenny",
                        Lastname = "Biggam",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "f230dc0c-3422-491a-889e-aa9901188605"
                    }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "3d0f2a03-d9f6-4855-84c4-aa9901188615",
                            Name = "GO OUTDOORS LTD (SHEFFIELD)",
                            CustomerReferenceNumber = "C1385805",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 3,
                            AgencyId = "f230dc0c-3422-491a-889e-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "c5a82bbc-977b-41a5-844d-aa990118861d",
                                Firstname = "Claire",
                                Lastname = "Neville",
                                IsDisabled = false,
                                ClientId = "3d0f2a03-d9f6-4855-84c4-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "bf69439b-f844-4313-a185-aa9901188618",
                                    Name = "Go Outdoors",
                                    IsDisabled = true,
                                    ClientId = "3d0f2a03-d9f6-4855-84c4-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "2b83b796-52f6-44de-8293-aa990118861a",
                                        Name = "Go Outdoors",
                                        IsDisabled = false,
                                        BrandId = "bf69439b-f844-4313-a185-aa9901188618"
                                    }
                                }
                            }
                        }
                    },
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "494a728a-c58b-408a-8734-aa9901188615",
                            Name = "Sony Music Entertainment",
                            CustomerReferenceNumber = "C1382484",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 3,
                            AgencyId = "f230dc0c-3422-491a-889e-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "0bedecf4-4e5a-442d-987c-aa990118861d",
                                Firstname = "Charles",
                                Lastname = "Wood",
                                IsDisabled = false,
                                ClientId = "494a728a-c58b-408a-8734-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "3f0fbadc-a7c5-4f9b-82fe-aa9901188618",
                                    Name = "Sony Recordings",
                                    IsDisabled = false,
                                    ClientId = "494a728a-c58b-408a-8734-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "458893e5-768d-4a4d-8368-aa990118861a",
                                        Name = "Sony Release",
                                        IsDisabled = true,
                                        BrandId = "3f0fbadc-a7c5-4f9b-82fe-aa9901188618"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            DisabledAgency = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "657aa32b-8d44-4de7-9043-aa9901188605",
                    Name = "Carat (North) Ltd - a trading division of Aegis Media",
                    CustomerReferenceNumber = "C2426347",
                    externalId = null,
                    IsDisabled = true
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "3821351c-97c4-4eca-82ca-aa9901188620",
                        Firstname = "Richard",
                        Lastname = "Smith",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "657aa32b-8d44-4de7-9043-aa9901188605"
                    }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "6500e5cf-2107-4f1e-9bf5-aa9901188615",
                            Name = "Centrale Shopping Centre",
                            CustomerReferenceNumber = "C2446261",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 2,
                            AgencyId = "657aa32b-8d44-4de7-9043-aa9901188605"
                        },
                        Contact = null,
                        BrandData = null
                    }
                },
            };

            ClientWithDisabledContact = new ClientData
            {
                Client = new Client
                {
                    Id = "6c8c42dd-8b01-4049-85be-aa9901188615",
                    Name = "THE DROC MANSFIELD LIMITED PARTNERSHIP",
                    CustomerReferenceNumber = "C1284338",
                    ExternalId = null,
                    IsDisabled = false,
                    TradingType = 3,
                    AgencyId = "f230dc0c-3422-491a-889e-aa9901188605"
                },
                Contact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "cd943a4c-9a77-4e45-91c7-aa990118861d",
                        Firstname = "Lenna",
                        Lastname = "xxx",
                        IsDisabled = true,
                        ClientId = "6c8c42dd-8b01-4049-85be-aa9901188615",
                        AgencyId = null
                    }
                },

                BrandData = null
            };

            DirectClientOnly = new ClientData
            {
                Client = new Client
                {
                    Id = "cef5a3ca-46f2-46ff-91e5-aa9901188603",
                    Name = "Emergency Property Services Ltd",
                    CustomerReferenceNumber = "C2432691",
                    ExternalId = null,
                    IsDisabled = false,
                    TradingType = 1,
                    AgencyId = null
                },
                Contact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "f1f54b4c-d62d-445c-befd-aa990118861d",
                        Firstname = "Kim",
                        Lastname = "Cole",
                        IsDisabled = false,
                        ClientId = "cef5a3ca-46f2-46ff-91e5-aa9901188603",
                        AgencyId = null
                    }
                },

                BrandData = new List<BrandData>
                {
                    new BrandData
                    {
                        Brand = new Brand
                        {
                            Id = "ef890a21-53d3-477b-bceb-aa9901188618",
                            Name = "Emergency Property Services",
                            IsDisabled = false,
                            ClientId = "cef5a3ca-46f2-46ff-91e5-aa9901188603"
                        },
                        Product = new List<Product>
                        {
                            new Product
                            {
                                Id = "f399474d-ffb1-4709-9c7f-aa990118861a",
                                Name = "Emergency Property Services",
                                IsDisabled = false,
                                BrandId = "ef890a21-53d3-477b-bceb-aa9901188618"
                            }
                        }
                    }
                }
            };

            ClientWithMultipleBrandsAndProducts = new ClientData
            {
                Client = new Client
                {
                    Id = "494a728a-c58b-408a-8734-aa9901188615",
                    Name = "Sony Music Entertainment",
                    CustomerReferenceNumber = "C1382484",
                    ExternalId = null,
                    IsDisabled = false,
                    TradingType = 3,
                    AgencyId = "f230dc0c-3422-491a-889e-aa9901188605"
                },
                Contact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "0bedecf4-4e5a-442d-987c-aa990118861d",
                        Firstname = "Charles",
                        Lastname = "Wood",
                        IsDisabled = false,
                        ClientId = "494a728a-c58b-408a-8734-aa9901188615",
                        AgencyId = null
                    }
                },

                BrandData = new List<BrandData>
                {
                    new BrandData
                    {
                        Brand = new Brand
                        {
                            Id = "4911fbcc-9c21-42b9-80e7-aa9901188618",
                            Name = "Black Butter Records",
                            IsDisabled = false,
                            ClientId = "494a728a-c58b-408a-8734-aa9901188615"
                        },
                        Product = new List<Product>
                        {
                            new Product
                            {
                                Id = "3210bb5d-d515-4366-a811-aa990118861b",
                                Name = "Zara Larsson",
                                IsDisabled = false,
                                BrandId = "4911fbcc-9c21-42b9-80e7-aa9901188618"
                            },
                            new Product
                            {
                                Id = "1184f5f6-50ad-4e4c-9941-aa990118861b",
                                Name = "DJ Khaled",
                                IsDisabled = false,
                                BrandId = "4911fbcc-9c21-42b9-80e7-aa9901188618"
                            }
                        }
                    },
                    new BrandData
                    {
                        Brand = new Brand
                        {
                            Id = "53395885-c38d-4bad-8df7-aa9901188618",
                            Name = "Calvin Harris",
                            IsDisabled = false,
                            ClientId = "494a728a-c58b-408a-8734-aa9901188615"
                        },
                        Product = new List<Product>
                        {
                            new Product
                            {
                                Id = "278553d2-7fc2-4354-8716-aa990118861a",
                                Name = "Calvin Harris",
                                IsDisabled = false,
                                BrandId = "53395885-c38d-4bad-8df7-aa9901188618"
                            }
                        }
                    }
                }
            };

            AgencyWithDisabledClient = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "ed218ab2-db37-4ff7-8820-aa9901188605",
                    Name = "ORGILL  LTD",
                    CustomerReferenceNumber = "C1292520",
                    externalId = null,
                    IsDisabled = false
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "bfef9924-5cd9-47f2-bf4d-aa9901188620",
                        Firstname = "Liz",
                        Lastname = "Orgill",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "ed218ab2-db37-4ff7-8820-aa9901188605"
                    }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "ae1e3f9c-58a0-40bd-bd3e-aa9901188615",
                            Name = "AIR SOUTHWEST (PLYMOUTH AIRPORT)",
                            CustomerReferenceNumber = "C1358218",
                            ExternalId = null,
                            IsDisabled = true,
                            TradingType = 3,
                            AgencyId = "ed218ab2-db37-4ff7-8820-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "25f680e7-0fac-4e30-9349-aa990118861d",
                                Firstname = "Mike",
                                Lastname = "Coombes",
                                IsDisabled = false,
                                ClientId = "ae1e3f9c-58a0-40bd-bd3e-aa9901188615",
                                AgencyId = null
                            }
                        },
                        BrandData = null
                    }
                }
            };

            AgencyWithDisabledContact = new AgencyData
            {
                Agency = new Agency
                {
                    Id = "56171ce0-4be6-44ac-8379-aa9901188605",
                    Name = "Mind Works Marketing Co Limited",
                    CustomerReferenceNumber = "C1350197",
                    externalId = null,
                    IsDisabled = false
                },
                AgencyContact = new List<Contact>
                {
                    new Contact
                    {
                        Id = "7644a6ab-a824-4ac6-b07f-aa9901188620",
                        Firstname = "Hayley",
                        Lastname = "Collins",
                        IsDisabled = true,
                        ClientId = null,
                        AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                    },
                    new Contact
                    {
                        Id = "9d75e510-8d39-4cfc-8b33-aa9901188620",
                        Firstname = "Michelle",
                        Lastname = "Leggatt",
                        IsDisabled = false,
                        ClientId = null,
                        AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                    }
                },
                ClientData = new List<ClientData>
                {
                    new ClientData
                    {
                        Client = new Client
                        {
                            Id = "3c833e81-7d4a-457f-b72a-aa9901188615",
                            Name = "THE MARLANDS SHOPPING CENTRE",
                            CustomerReferenceNumber = "C1410078",
                            ExternalId = null,
                            IsDisabled = false,
                            TradingType = 3,
                            AgencyId = "56171ce0-4be6-44ac-8379-aa9901188605"
                        },
                        Contact = new List<Contact>
                        {
                            new Contact
                            {
                                Id = "c5c0d298-01a3-40cc-ac00-aa990118861d",
                                Firstname = "Deepeka",
                                Lastname = "Dayal",
                                IsDisabled = false,
                                ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615",
                                AgencyId = null
                            },
                            new Contact
                            {
                                Id = "6a1c7121-a2e9-46c8-9cd8-aa990118861d",
                                Firstname = "Tim",
                                Lastname = "Keeping",
                                IsDisabled = false,
                                ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615",
                                AgencyId = null
                            }
                        },

                        BrandData = new List<BrandData>
                        {
                            new BrandData
                            {
                                Brand = new Brand
                                {
                                    Id = "6c60d5d7-c12a-45f8-acc8-aa9901188618",
                                    Name = "The Marlands Shopping Centre",
                                    IsDisabled = false,
                                    ClientId = "3c833e81-7d4a-457f-b72a-aa9901188615"
                                },
                                Product = new List<Product>
                                {
                                    new Product
                                    {
                                        Id = "2ad03fa0-af9a-4a3b-9f82-aa990118861a",
                                        Name = "The Marlands Shopping Centre",
                                        IsDisabled = false,
                                        BrandId = "6c60d5d7-c12a-45f8-acc8-aa9901188618"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            OwnerOnly = new List<People>
            {
                new People
                {
                    Id = "fbbdddee-6bda-4d33-8e84-aa9500a6c839",
                    Firstname = "Amy",
                    Lastname = "Fillery",
                    IsDisabled = false,
                    Capabilities = new List<int> {1}
                },
                new People
                {
                    Id = "7be28538-74f9-4a0a-b290-aa9500a6c839",
                    Firstname = "Danielle",
                    Lastname = "Jeffers",
                    IsDisabled = false,
                    Capabilities = new List<int> {1}
                }

            };

            OwnerAndPlanner = new List<People>
            {
                new People
                {
                    Id = "90c44cae-2a4a-4b5d-a5ec-aa9500a6c839",
                    Firstname = "Adam",
                    Lastname = "Szabo",
                    IsDisabled = false,
                    Capabilities = new List<int> {1, 2}
                }
            };

            PlannerOnly = new List<People>
            {
                new People
                {
                    Id = "3f001cb9-24c3-4dc2-9b5b-aa9500a6c839",
                    Firstname = "Andrew",
                    Lastname = "Vogel",
                    IsDisabled = false,
                    Capabilities = new List<int> {2}
                }
            };

            DisabledOwnerAndPlanner = new List<People>
            {
                new People
                {
                    Id = "ca4b5047-0d8b-4d69-a5ea-aa9500a6c839",
                    Firstname = "Jack",
                    Lastname = "Gillard",
                    IsDisabled = true,
                    Capabilities = new List<int> {1, 2}
                }
            };

            USCurrency = new Currency
            {
                name = "US Dollar",
                isoCurrencySymbol = "USD",
                symbol = "$",
                IsDisabled = true               
            };

            AUSCurrency = new Currency
            {
                name = "Australian Dollar",
                isoCurrencySymbol = "AUD",
                symbol = "$",
                IsDisabled = false
            };

            switch (environment.ToLower())
            {
                case ("int"):
                    AUSCurrency.Id = "f1c834ac-9fe2-4900-9b59-ab1100f3c381";
                    USCurrency.Id = "d3568f32-f839-4e29-ade4-aac600ef1626";
                    break;
                case ("test"):
                    AUSCurrency.Id = "";
                    USCurrency.Id = "";
                    break;
            }
            
        }
    }
}
