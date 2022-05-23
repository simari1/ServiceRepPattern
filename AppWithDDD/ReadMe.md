# 書籍のサンプルの写経
ドメイン駆動設計入門 ボトムアップでわかる！ドメイン駆動設計の基本
https://www.shoeisha.co.jp/book/detail/9784798150727

GitHubのソース
https://github.com/nrslib/itddd

第11-13章のサークル管理サンプル
そのほか気になる点やメモ等諸々。

## 要件
- サークル名は3文字以上、20文字以下
- サークル名はすべてのサークルで重複しない
- サークルに所属するユーザーの最大数はサークルのオーナーとなるユーザーを含めて30名まで
- プレミアユーザーが10名以上所属しているサークルはユーザーの最大数は50名まで
- おすすめサークル検索機能、
    - 直近1か月以内に結成されたサークル
    - 所属メンバーが10名以上

## 設計の順番
1. どういう機能が求められているのか。
2. 機能を成り立たせるためのユースケースの洗い出し。
3. 必要となる概念とルールから業務知識を抜き出し、ドメインオブジェクトを選定する。

## 実装の順番
1. Value Object
2. Domain Model
3. RepositoryのInterface     （先にInterfaceを作って、実処理は後回しでOK）
4. FactoryのInterface
5. Command Object
6. Application Service
7. それ以降
	- Applicatioin Serviceのユースケース作成（Command Object - Domain Object）。
	- 仕様クラス作成。

### Tips：
- Value Object
Value ObjectにDomainModelの単純な業務知識のルールをまとめる。
Userだったら、Id、NameはNullではだめ、とか。
https://zenn.dev/chida/articles/aa2a63cdf2eb52

- Domain Model
関連のある値とふるまいのセットをクラスにまとめる。Self documentingなクラスを作成する。
https://stg-tud.github.io/eise/WS11-EiSE-07-Domain_Modeling.pdf
Domain, User Caseを言葉で説明して出てくる名詞がDomain Modelの候補になる。

- Repository
Infrastructureは相互変換可能なもの。RepositoryクラスのようなクラスをInterfaceにまとめて汎用的にServiceから呼び出せるように管理する。
DI、ServiceLocatorを利用し単体テストもでしやすくする。
https://www.nuits.jp/entry/servicelocator-vs-dependencyinjection#ServiceLocator%E3%81%A8Dependency-Injection-%E3%81%84%E3%81%9A%E3%82%8C%E3%82%92%E5%88%A9%E7%94%A8%E3%81%99%E3%81%B9%E3%81%8D%E3%81%8B
https://webbibouroku.com/Blog/Article/cs-di-servicecollection
その際単体テストの範囲は以下になる。Repositoryをテストするのは結合テスト。
	- DomainModelの業務知識のロジック。
	- DomainService, ApplicationServiceにInMemoryのRepositoryを渡して通しのテスト。

- Factory
コンストラクタが複雑で初期化処理が複雑なオブジェクトの場合はFactoryを導入することを検討してもよい。
	- 他のオブジェクトをインスタンス化する。
	- 採番処理等Repositoryを見に行くようなオブジェクト。

- Domain Service
同じ型の複数のドメインオブジェクトをまたぐ際に必要になる処理、状態の記録等が必要な処理をまとめる（Domain ObjectがRepositoryへアクセスするのはダメ）。（P.68）
例：
	- ユーザーの重複チェック処理。JohnDoe Objectが自身があるか問い合わせをするのは不自然。この場合UserServiceを作る。
	- 拠点間の配送処理。状態の記録が必要。この場合TransportServiceを作る。

- Application Service
ビジネスシナリオの進行役。Domain Oject, Domain Service, Repository, Factory を利用して処理する。
ここに業務知識を含ませてはいけない！

- Application Service、Domain Serviceに本来Domain Modelにある処理が漏れ出してしまうと、Domain Model貧血症になる。
Serviceにあると不自然な処理、重複が生じてしまう処理を抽出して、Domain Modelにふるまいとして入れられないか検討する。
例：
Circleのメンバー追加で、Application Serviceの中でcircle.members.add(user)で毎回人数の上限チェックを行っていたが、Circleクラスに持たせるべき。
Application ServiceからDomain Objectのpublicのプロパティであるmemberを他のクラスが直接参照・編集するのはよくない（Setter, Getter不要説）。
	- BAD
	if (circle.Members.Count >= 29) throw new CircleFullException(id);
	circle.Members.Add(member);
	- GOOD
	CircleのふるまいとしてIsFullメソッドを作り、CircleのJoinのメソッドの中でIsFullのチェックも呼ぶ。memberもprivateになっている。
	memberがpublicだと、ApplicationService側で上限チェック等のビジネスロジックが漏れ出してしまう恐れがある。
	circle.Join(member);

- 業務知識が漏洩しないように、DomainModelのフィールドはできるだけClosedにした方がいい。しかしDomainSetter、GetterをなくしてPrivateにしたら、EF等のORMのEntityとのマッピングが大変になる。どうする？
→もうそこは割り切ってPublicにするしかないんじゃないかな、、、Reflectionを使うことも考えられるが、そこまでするか？
https://stackoverflow.com/questions/52373148/ddd-not-exposing-getters-while-having-mapper-class

- クラスの凝集度の確認観点。定義されているクラスレベルのFieldがメソッドの中で使われていないものがあれば、それは分けるべき。
●●Serviceのすべてのメソッドで●●Repositoryは使われているか？
一つだけ××Repositoryを使っているようなメソッドがあればそれは分けてもいいかもれない。

- Domain Modelの中から入出力のRepositoryを呼ぶのはありなのか？
→ 基本無し。ドメインモデルが太りすぎるのも正しくない。ドメインモデルにはビジネス仕様のみが記載されるべき。
解決1: CircleFullSpecificationを導入する。Utilクラスみたいなもんだけど、仕様というドメインオブジェクトを作ること。(P.297)
解決2: ファーストクラスコレクションを作る。

- Repositoryクラスに複数Find●●Circleみたいな特殊なメソッドが増えていくのはありなのか？
→ RepositoryはInfra層のものなので そこにFindRecommendedCircleみたいな業務知識、仕様を含むメソッドを作るのは避けるべき。
解決1: それこそ上で記載している仕様クラスにするべき。
解決2: Repositoryメソッドに汎用的に利用できるFindメソッドを作っておく。（P. 305-307）
FindメソッドはInterfaceで検索条件を受け取るようにして置き、必要に応じた検索条件を渡すようにする。実際だったらFunctionメソッドを引き渡す感じになるのかな。

- UIに業務知識を持たせない。
あるデータのリストで特定のプロパティに応じて表示・非表示させたい。
	- BAD
	UIの中でif文を使い表示・非表示を切り替える。
	- GOOD
	データのプロパティにdisplayタグの値（none, inline等）を持たせてしまう。

- Layered Architecture
フォルダ分けするときは、異なるオブジェクトのRepositoriesを混ぜるようなことはしない。フォルダ分けする単位もドメインで分ける。
	- Presetation
	- Application
		- Create, Delete等ユースケースごとに分ける
	- Domain
		- Models: Entities, Value Object, Domain Object,Factory, Repository, Specification
		- Services
		- Shared
	- Infrastructure

#### そのほか
- IQueryableとIEnumerableの違いを意識する。
https://www.c-sharpcorner.com/article/difference-between-iqueryable-and-ienumerable-while-working-in-linq/
> IEnumerable is useful when we want to iterate the collection of objects which deals with in-process memory.
> IQueryable is useful when we want to iterate a collection of objects which deals with ad-hoc queries against the data source or remote database, like SQL Server.
> IList is useful when we want to perform any operation like Add, Remove or Get item at specific index position in the collection.

DBへのクエリだったらIQueryableの方がいい。
https://stackoverflow.com/questions/8947923/iqueryable-vs-ienumerable-in-the-repository-pattern-lazy-loading
IEnumerableはすべてのデータを一回クライアントに持ってきて、そこからフィルタリングする。IQUeryableはSelect文を発行するので、必要な分だけのデータが返ってくる。


## 感想
- ただ単純にサンプルコードがファイル単位でバラバラになっている・・
できればConsoleアプリかASP.Netで動くアプリにしてほしかった。フォルダ構成をどうするべきとかがわからない。FactoryとRepositoryとDomain Serviceを同じフォルダにしてしまうのとかダメな気がする。
（このソースもフォルダ構成は後で変更した方がいいかも。）

- モデルの数が多すぎる。ValueObject、DomainObject、ORMのエンティティ、View用のModel、CommandObject、モデルの数が多くなってしまうからどこかで妥協が必要。
ValueObject、DomainObjectを使っている時点でORMのエンティティにはマッピングできない。
DomainObjectがそのままView用のModelに使えない。
Automapperみたいなものを使うしかないが。。。
https://atmarkit.itmedia.co.jp/ait/articles/1503/17/news115.html

- ValueObjectに業務知識がまとまっているのが、毎回メソッドの頭でNullCheckとかしなくてよくて良い感じ。

- RepositoryクラスではSQLべた書きで書くより、Entity Frameworkを使ってほしかった。MSの勉強動画を参照すると、EF使わないでSQLべた書きしているのはあまりない。
