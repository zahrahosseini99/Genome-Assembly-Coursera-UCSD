#python3
def de_bruijn(k, n: int) -> str:

    alphabet = list(map(str, range(k)))
    temp = [0] * k * n
    arr = []

    def dfs(i, level):
        if i > n:
            if n % level == 0:
                arr.extend(temp[1 : level + 1])
        else:
            temp[i] = temp[i - level]
            dfs(i + 1, level)
            for j in range(temp[i - level] + 1, k):
                temp[i] = j
                dfs(i + 1, i)

    dfs(1, 1)
    return "".join(alphabet[i] for i in arr)

k_uni=input()
number=2
print(de_bruijn(number,int(k_uni)))
