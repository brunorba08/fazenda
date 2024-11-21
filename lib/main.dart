import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';


class Product {
  final String name;
  final String price;
  final String image;


  Product({
    required this.name,
    required this.price,
    required this.image,

  });
}


void main() {
  runApp(
    ChangeNotifierProvider(
      create: (context) => CartProvider(),
      child: GreenCityApp(), // Seu widget principal
    ),
  );
}

class GreenCityApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'GreenCity',
      theme: ThemeData(
        primarySwatch: Colors.green,
        scaffoldBackgroundColor: Colors.green[50],
        fontFamily: 'Roboto',
      ),
      initialRoute: '/',
      routes: {
        '/': (context) => HomePage(),
        '/cart': (context) => CartPage(),
        '/login': (context) => LoginPage(),
        '/favorites': (context) => FavoritesPage(),
        '/delivery': (context) => DeliveryPage(),
        '/payment': (context) => PaymentPage(),
        '/register': (context) => RegisterPage(),
      },
    );
  }
}



class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    // Definindo uma lista de produtos
    List<Map<String, String>> products = [
      {
        'name': 'Alface',
        'price': 'R\$ 49,90/15und',
        'image': 'IMAGENS/alface.png',
      },
      {
        'name': 'Coentro',
        'price': 'R\$ 39,90/15und',
        'image': 'IMAGENS/coentro.png',
      },
      {
        'name': 'Couve Flor 250g',
        'price': 'R\$ 49,90/15und',
        'image': 'IMAGENS/couvef.png',
      },
      {
        'name': 'Banana',
        'price': 'R\$ 12,00',
        'image': 'IMAGENS/couvef.png',
      },
      {
        'name': 'Laranja',
        'price': 'R\$ 9,00',
        'image': 'IMAGENS/alface.png',
      },
      {
        'name': 'Maçã',
        'price': 'R\$ 8,00',
        'image': 'IMAGENS/coentro.png',
      },
    ];

    return Scaffold(
      appBar: AppBar(
        title: Row(
          children: [
            Image.asset('IMAGENS/logo.png', height: 20),
            SizedBox(width: 5),
            Expanded(
              child: TextField(
                decoration: InputDecoration(
                  hintText: 'Buscar produtos...',
                  prefixIcon: Icon(Icons.search),
                  suffixIcon: Icon(Icons.mic),
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(20),
                  ),
                  filled: true,
                  fillColor: Colors.white,
                ),
              ),
            ),
          ],
        ),
        actions: [
          IconButton(
            icon: Icon(Icons.person_add),
            onPressed: () => Navigator.pushNamed(context, '/register'),
          ),
          IconButton(
            icon: Icon(Icons.login),
            onPressed: () => Navigator.pushNamed(context, '/login'),
          ),
          IconButton(
            icon: Icon(Icons.favorite),
            onPressed: () => Navigator.pushNamed(context, '/favorites'),
          ),
          IconButton(
            icon: Icon(Icons.shopping_cart),
            onPressed: () => Navigator.pushNamed(context, '/cart'),
          ),
        ],
        backgroundColor: Colors.green[700],
      ),
      body: Column(
        children: [
          SizedBox(height: 20),
          Container(
            margin: EdgeInsets.symmetric(horizontal: 16),
            padding: EdgeInsets.all(16),
            decoration: BoxDecoration(
              color: Colors.green[100],
              borderRadius: BorderRadius.circular(10),
            ),
            child: Text(
              'Bem-vindo à Fazenda GreenCity! Confira nossas melhores ofertas!',
              textAlign: TextAlign.center,
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
                color: Colors.green[800],
              ),
            ),
          ),
          SizedBox(height: 15),
          Expanded(
            child: GridView.builder(
              padding: EdgeInsets.symmetric(
                  horizontal: MediaQuery.of(context).size.width * 0.05),
              gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                crossAxisCount: MediaQuery.of(context).size.width > 600 ? 3 : 2,
                childAspectRatio: 0.8,
                crossAxisSpacing: 10,
                mainAxisSpacing: 10,
              ),
              itemCount: products.length,
              itemBuilder: (context, index) {
                var product = products[index];
                return ProductCard(
                  name: product['name']!,
                  price: product['price']!,
                  image: product['image']!,
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => ProductDetailsPage(
                          productName: product['name']!,
                          productPrice: product['price']!,
                          productImage: product['image']!, onTap: () {  },
                        ),
                      ),
                    );
                  },
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}

class ProductCard extends StatelessWidget {
  final String name;
  final String price;
  final String image;
  final VoidCallback onTap;

  ProductCard({
    required this.name,
    required this.price,
    required this.image,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Card(
        child: Column(
          children: [
            Image.asset(
              image,
              height: 100,
              width: 100,
              fit: BoxFit.cover,
            ),
            SizedBox(height: 10),
            Text(name, style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
            Text(price, style: TextStyle(fontSize: 14, color: Colors.green[800])),
          ],
        ),
      ),
    );
  }
}

class ProductDetailsPage extends StatefulWidget {
  final String productName;
  final String productPrice;
  final String productImage;
  final VoidCallback onTap;

  ProductDetailsPage({
    Key? key,
    required this.productName,
    required this.productPrice,
    required this.productImage,
    required this.onTap,
  }) : super(key: key);

  @override
  _ProductDetailsPageState createState() => _ProductDetailsPageState();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(productName),
        backgroundColor: Colors.green[700],
      ),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Image.asset(productImage, height: 200),
          SizedBox(height: 20),
          Text(
            productName,
            style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
          ),
          Text(
            productPrice,
            style: TextStyle(fontSize: 20, color: Colors.green[700]),
          ),
        ],
      ),
    );
  }
}


  @override
  Widget build(BuildContext context) {
    var onTap;
    var name;
    var price;
    return GestureDetector(
      onTap: onTap,
      child: Card(
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
        elevation: 5,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Image.asset("IMAGENS/alface", height: 80, fit: BoxFit.cover),
            SizedBox(height: 10),
            Text(
              name,
              style: TextStyle(fontWeight: FontWeight.bold),
              textAlign: TextAlign.center,
            ),
            Text(
              price,
              style: TextStyle(color: Colors.green[700]),
            ),
          ],
        ),
      ),
    );
  }

class _ProductDetailsPageState extends State <ProductDetailsPage> {
  int quantity = 1;
  List<Map<String, dynamic>> cart = []; // Lista do carrinho

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.productName),
      ),
      body: Column(
        children: [
          Image.asset(widget.productImage, height: 200, fit: BoxFit.cover),
          SizedBox(height: 20),
          Text(
            widget.productName,
            style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
          ),
          SizedBox(height: 10),
          Text(
            widget.productPrice,
            style: TextStyle(fontSize: 20, color: Colors.green[700]),
          ),
          SizedBox(height: 20),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              IconButton(
                icon: Icon(Icons.remove),
                onPressed: () {
                  if (quantity > 1) {
                    setState(() {
                      quantity--;
                    });
                  }
                },
              ),
              Text(
                '$quantity',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              IconButton(
                icon: Icon(Icons.add),
                onPressed: () {
                  setState(() {
                    quantity++;
                  });
                },
              ),
            ],
          ),
          SizedBox(height: 20),
          ElevatedButton(
            onPressed: () {
              setState(() {
                cart.add({
                  'name': widget.productName,
                  'price': widget.productPrice,
                  'quantity': quantity,
                  'image': widget.productImage,
                });
              });
              ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                content: Text('${widget.productName} adicionado ao carrinho!'),
              ));
            },
            child: Text('Adicionar ao Carrinho'),
          ),
        ],
      ),
    );
  }
}


// Página de login
class LoginPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Login'),
        backgroundColor: Colors.green[700],
      ),
      body: Padding(
        padding: EdgeInsets.symmetric(
            horizontal: MediaQuery.of(context).size.width *
                0.05), // Removido o ponto e vírgula
        child: Column(
          children: [
            TextField(
              decoration: InputDecoration(
                labelText: 'Email',
                border: OutlineInputBorder(),
              ),
            ),
            SizedBox(height: 20),
            TextField(
              obscureText: true,
              decoration: InputDecoration(
                labelText: 'Senha',
                border: OutlineInputBorder(),
              ),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                // Lógica de login
              },
              child: Text('Entrar'),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.green[700],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

// Cadastro
class RegisterPage extends StatefulWidget {
  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  bool isEmailOffersChecked = false;
  bool isSmsOffersChecked = false;
  bool isTermsAccepted = false;

  final TextEditingController nomeFantasiaController = TextEditingController();
  final TextEditingController nomeSocialController = TextEditingController();
  final TextEditingController cnpjController = TextEditingController();
  final TextEditingController emailController = TextEditingController();
  final TextEditingController telefoneController = TextEditingController();

  bool _isLoading = false;

  Future<void> cadastrarCliente() async {
    // Verificar se os campos estão preenchidos
    if (nomeFantasiaController.text.isEmpty ||
        nomeSocialController.text.isEmpty ||
        cnpjController.text.isEmpty ||
        emailController.text.isEmpty ||
        telefoneController.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text('Por favor, preencha todos os campos!'),
        backgroundColor: Colors.red,
      ));
      return;
    }

    // Ativar o indicador de carregamento
    setState(() {
      _isLoading = true;
    });

    try {
      final response = await http.post(
        Uri.parse('http://seu-servidor-api/cadastrar_cliente'), // Substitua pela URL da sua API
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode({
          'nome_fantasia': nomeFantasiaController.text,
          'nome_social': nomeSocialController.text,
          'cnpj': cnpjController.text,
          'email': emailController.text,
          'telefone': telefoneController.text,
        }),
      );

      // Verifica o código de status da resposta
      if (response.statusCode == 200) {
        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          content: Text('Cadastro realizado com sucesso!'),
          backgroundColor: Colors.green,
        ));
        Navigator.pushNamed(context, '/login'); // Navega para a tela de login após o cadastro
      } else {
        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          content: Text('Falha ao cadastrar. Tente novamente.'),
          backgroundColor: Colors.red,
        ));
      }
    } catch (e) {
      // Tratamento de erro em caso de falha na requisição
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text('Erro ao se conectar ao servidor. Tente novamente.'),
        backgroundColor: Colors.red,
      ));
    } finally {
      // Desativar o indicador de carregamento
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Cadastro'),
        backgroundColor: Colors.green[700],
      ),
      body: Padding(
        padding: EdgeInsets.symmetric(
            horizontal: MediaQuery.of(context).size.width * 0.05),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              TextField(
                controller: nomeFantasiaController,
                decoration: InputDecoration(
                  labelText: 'Nome Fantasia',
                  border: OutlineInputBorder(),
                ),
              ),
              SizedBox(height: 16),
              TextField(
                controller: nomeSocialController,
                decoration: InputDecoration(
                  labelText: 'Nome Social',
                  border: OutlineInputBorder(),
                ),
              ),
              SizedBox(height: 16),
              TextField(
                controller: cnpjController,
                decoration: InputDecoration(
                  labelText: 'CNPJ',
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.number,
              ),
              SizedBox(height: 16),
              TextField(
                controller: emailController,
                decoration: InputDecoration(
                  labelText: 'Email',
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.emailAddress,
              ),
              SizedBox(height: 16),
              TextField(
                controller: telefoneController,
                decoration: InputDecoration(
                  labelText: 'Telefone',
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.phone,
              ),
              SizedBox(height: 16),
              Row(
                children: [
                  Checkbox(
                    value: isEmailOffersChecked,
                    onChanged: (value) {
                      setState(() {
                        isEmailOffersChecked = value!;
                      });
                    },
                  ),
                  Expanded(
                    child: Text('Aceito receber ofertas por Email'),
                  ),
                ],
              ),
              Row(
                children: [
                  Checkbox(
                    value: isSmsOffersChecked,
                    onChanged: (value) {
                      setState(() {
                        isSmsOffersChecked = value!;
                      });
                    },
                  ),
                  Expanded(
                    child: Text('Aceito receber ofertas por SMS'),
                  ),
                ],
              ),
              Row(
                children: [
                  Checkbox(
                    value: isTermsAccepted,
                    onChanged: (value) {
                      setState(() {
                        isTermsAccepted = value!;
                      });
                    },
                  ),
                  Expanded(
                    child: Text('Aceito os Termos e Condições'),
                  ),
                ],
              ),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: isTermsAccepted && !_isLoading
                    ? () {
                  cadastrarCliente(); // Chama a função para cadastrar
                }
                    : null,
                child: _isLoading
                    ? CircularProgressIndicator(color: Colors.white) // Indicador de carregamento
                    : Text('Cadastrar'),
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.green[700],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

// Exemplo de carrinho


// Página de favoritos
class FavoritesPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Favoritos'),
        backgroundColor: Colors.green[700],
      ),
      body: Center(
        child: Text('Seus produtos favoritos'),
      ),
    );
  }
}



void mai() {
  runApp(
    ChangeNotifierProvider(
      create: (context) => CartProvider(),
      child: MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: ProductPage(), // Substitua por sua página inicial de produtos
    );
  }
}

// Modelo de Produto



// Provider do Carrinho
class CartProvider with ChangeNotifier {
  List<Product> _cart = [];

  List<Product> get cart => _cart;

  double get totalPrice => _cart.fold(0, (total, item) => total + double.parse(item.price));

  void addProduct(Product product) {
    _cart.add(product);
    notifyListeners();
  }

  void removeProduct(Product product) {
    _cart.remove(product);
    notifyListeners();
  }
}

// Página de Produtos (apenas exemplo para teste)
class ProductPage extends StatelessWidget {
  final List<Product> products = [
    Product(name: 'Produto 1', price: '10.00', image: 'assets/product1.png'),
    Product(name: 'Produto 2', price: '20.00', image: 'assets/product2.png'),
  ];

  @override
  Widget build(BuildContext context) {
    final cartProvider = Provider.of<CartProvider>(context);

    return Scaffold(
      appBar: AppBar(
        title: Text('Produtos'),
        backgroundColor: Colors.green[700],
        actions: [
          IconButton(
            icon: Icon(Icons.shopping_cart),
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => CartPage()),
              );
            },
          ),
        ],
      ),
      body: ListView.builder(
        itemCount: products.length,
        itemBuilder: (context, index) {
          final product = products[index];
          return ListTile(
            leading: Image.asset(product.image, width: 50),
            title: Text(product.name),
            subtitle: Text('R\$ ${product.price}'),
            trailing: ElevatedButton(
              onPressed: () {
                cartProvider.addProduct(product);
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('${product.name} adicionado ao carrinho!')),
                );
              },
              child: Text('Adicionar'),
            ),
          );
        },
      ),
    );
  }
}

// Página do Carrinho
class CartPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final cartProvider = Provider.of<CartProvider>(context);

    return Scaffold(
      appBar: AppBar(
        title: Text('Carrinho'),
        backgroundColor: Colors.green[700],
      ),
      body: cartProvider.cart.isEmpty
          ? Center(child: Text('Seu carrinho está vazio!'))
          : Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: cartProvider.cart.length,
              itemBuilder: (context, index) {
                final product = cartProvider.cart[index];
                return ListTile(
                  leading: Image.asset(product.image, width: 50),
                  title: Text(product.name),
                  subtitle: Text('R\$ ${product.price}'),
                  trailing: IconButton(
                    icon: Icon(Icons.remove_shopping_cart, color: Colors.red),
                    onPressed: () {
                      cartProvider.removeProduct(product);
                      ScaffoldMessenger.of(context).showSnackBar(
                        SnackBar(content: Text('${product.name} removido do carrinho!')),
                      );
                    },
                  ),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              children: [
                Text(
                  'Total: R\$ ${cartProvider.totalPrice.toStringAsFixed(2)}',
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                ),
                SizedBox(height: 10),
                ElevatedButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => DeliveryPage()),
                    );
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.green[700],
                    padding: EdgeInsets.symmetric(horizontal: 24, vertical: 12),
                  ),
                  child: Text(
                    'Ir para entrega',
                    style: TextStyle(fontSize: 16),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Página de Entrega
class DeliveryPage extends StatefulWidget {
  @override
  _DeliveryPageState createState() => _DeliveryPageState();
}

class _DeliveryPageState extends State<DeliveryPage> {
  String selectedDeliveryMethod = 'Padrão (5-7 dias úteis)';
  String address = 'Rua Exemplo, 123, Bairro, Cidade, Estado';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Entrega'),
        backgroundColor: Colors.green[700],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Escolha o endereço de entrega',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 20),
            // Detalhes de endereço e método de entrega
            Spacer(),
            ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => PaymentPage()),
                );
              },
              child: Text('Confirmar Entrega'),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.green[700],
                padding: EdgeInsets.symmetric(vertical: 15),
                textStyle: TextStyle(fontSize: 18),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

// Página de Pagamento
class PaymentPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Pagamento'),
        backgroundColor: Colors.green[700],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            // Escolha do método de pagamento
            Spacer(),
            ElevatedButton(
              onPressed: () {
                // Finalizar compra
              },
              child: Text('Finalizar Pagamento'),
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.green[700],
                padding: EdgeInsets.symmetric(vertical: 15),
                textStyle: TextStyle(fontSize: 18),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
