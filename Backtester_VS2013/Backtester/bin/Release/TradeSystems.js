{"tradeSystems":[{"condicaoEntradaC":"GREATER(MME(C,9),MME(C,6))","condicaoEntradaV":"LOWER(MME(C,9),MME(C,6))","condicaoSaidaC":"LOWER(MME(C,9),MME(C,6))","condicaoSaidaV":"GREATER(MME(C,9),MME(C,6))","name":"TradeSystem Novo...0","stopInicialC":"SUBTRACT(L,MULTIPLY(STD(C,10),2))","stopInicialV":"SUM(H,MULTIPLY(STD(C,10),2))","stopMovelC":"SUBTRACT(L,MULTIPLY(STD(C,10),2))","stopMovelV":"SUM(H,MULTIPLY(STD(C,10),2))","vm":{"variaveis":[{"descricao":"Usado para dar algum espaçamento entre a formula e o valor realmente usado","name":"STOP_GAP","steps":5,"uniqueID":1,"vlrAtual":0,"vlrFinal":20,"vlrInicial":0},{"descricao":"Valor percentual máximo que um trade pode ter de risco","name":"RISCO_TRADE","steps":5,"uniqueID":2,"vlrAtual":0,"vlrFinal":20,"vlrInicial":0},{"descricao":"Valor percentual máximo que pode ficar exposto","name":"RISCO_GLOBAL","steps":5,"uniqueID":3,"vlrAtual":0,"vlrFinal":20,"vlrInicial":0},{"descricao":"Valor percentual que caso atingido implica em paralisar as operações para aquele mês","name":"STOP_MENSAL","steps":5,"uniqueID":4,"vlrAtual":0,"vlrFinal":30,"vlrInicial":0},{"descricao":"Valor absoluto máximo que um trade pode ter","name":"MAX_CAPITAL_TRADE","steps":5,"uniqueID":5,"vlrAtual":10000,"vlrFinal":100000,"vlrInicial":10000},{"descricao":"informa quanto do capital eu posso ter por ativo","name":"PERC_TRADE","steps":5,"uniqueID":6,"vlrAtual":10,"vlrFinal":100,"vlrInicial":10},{"descricao":"Usa stop movel? 0=nao, 1=sim","name":"USA_STOP_MOVEL","steps":1,"uniqueID":7,"vlrAtual":0,"vlrFinal":1,"vlrInicial":0},{"descricao":"A posição pode ter mais de uma entrada(operacao)? 0=nao, 1=sim","name":"MULTIPLAS_ENTRADAS","steps":1,"uniqueID":8,"vlrAtual":0,"vlrFinal":1,"vlrInicial":0}]}}]}