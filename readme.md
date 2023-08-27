● ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
- La relación entre cadetería y cadetes sería de composición, ya que si elimino la cadetería, no me interesa tener los clientes, ya que los clientes dependen de la cadeteria.
- La relación entre los pedidos y los cadetes, vendría a ser de agregación, ya que en caso de desaparecer el cadete, el pedido no debería desaparecer, sino que debería ser asignado a otro cadete
- La relación entre los pedidos y cliente, vendría a ser de composición, ya que en caso de que se elimine el pedido, el cliente no se reasigna a otra página para que realice otro pedido, sino que desaparece

● ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
- La clase cadetería, debería tener también los métodos:
    - CrearPedido()
    - ActualizarPedido()
    - ReasingarPedido()
    - EliminarPedido()
    - AgregarCadete()
    - BorrarCadete()
- La clase Cadete, debe tener los métodos:
    - CrearPedido()
    - ActualizarPedido()
    - EliminarPedido()
● Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
- Deberían ser privados:
    - De pedidos:
        - Cliente, porque es el único atributo que el cliente no necesitaría ver
    - De Cliente ninguno necesariamente
    - De Cadete
        - Listado de Pedidos
    - De cadeteria
        - ListadoCadetes
● ¿Cómo diseñaría los constructores de cada una de las clases?
- Los diseñaria de varias formas (varios constructores con distintos parámetros), para reutilizar código y evitar errores
● ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?

